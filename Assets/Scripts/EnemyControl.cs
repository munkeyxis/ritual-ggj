using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {

	public Vector2 speed = new Vector2(50,50);
	public GameObject shot;
	public Vector2 shotSpeed;
	public float shotInterval=1.0f;
	public float minDist;
	public Transform splode;
	private Vector2 movement;
	private Rigidbody2D phys;
	private float nextShot=0;
	private Animator animator;

	private readonly Vector2 up = new Vector2(0,1);
	private readonly Vector2 left = new Vector2(-1,0);
	private readonly Vector2 right = new Vector2(1,0);
	private readonly Vector2 down = new Vector2(0,-1);

	private bool dodging = false;
	private Rigidbody2D dodgeWhat;
	private Vector2 dodgestart;

	// Use this for initialization
	void Start () {
		phys = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
	}

	bool CheckShot(Vector2 dir)
	{
		Rigidbody2D shotPhys;

		RaycastHit2D hasShot;

		Vector2 origin = new Vector2(transform.position.x + dir.x,transform.position.y+dir.y);

		hasShot = Physics2D.Raycast (transform.position, dir, Mathf.Infinity,(1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("PlayerShot")) | (1 << LayerMask.NameToLayer("Obstacle")));
		
		if (hasShot.collider != null) {
			if (hasShot.collider.GetComponent<PlayerControl>()!=null) {
				return true;
			} else if (hasShot.collider.GetComponent<ShotScript>()!=null) {
				shotPhys = hasShot.collider.GetComponent<Rigidbody2D> ();
				if ((shotPhys.velocity.y < 0)) {
					dodging = true;
					dodgeWhat = shotPhys;
					dodgestart = transform.position;
				}
			}
		}
		return false;
	}

	// Update is called once per frame
	void Update () {

		Vector2 shotDir = new Vector2(0,0);

		nextShot += Time.deltaTime;

		// Do we take a shot?

		if (CheckShot (up)) {
			shotDir = new Vector2 (0, shotSpeed.y);
		} else if (CheckShot (down)) {
			shotDir = new Vector2 (0, -shotSpeed.y);
		} else if (CheckShot (left)) {
			shotDir = new Vector2 (-shotSpeed.x, 0);
		} else if (CheckShot (right)) {
			shotDir = new Vector2 (shotSpeed.x, 0);
		}

		if (shotDir != new Vector2(0,0) && nextShot>shotInterval) {
			nextShot = 0;
			GameObject newShot = Instantiate (shot) as GameObject;
			Rigidbody2D shotPhys = newShot.GetComponent<Rigidbody2D> ();
			Destroy (newShot,3);
			shotPhys.velocity = shotDir;
			newShot.transform.position = new Vector2(transform.position.x + shotDir.x/shotSpeed.x,transform.position.y+shotDir.y/shotSpeed.y);
			//SFX
		}

		// Do We Move?

		bool doClose;
		float xdist, ydist;

		if (Vector2.Distance (transform.position, PlayerControl.instance.transform.position) > minDist) {
			doClose = true;
		} else {
			doClose = false;
		}

		xdist = PlayerControl.instance.transform.position.x - transform.position.x;
		ydist = PlayerControl.instance.transform.position.y - transform.position.y;

		movement = new Vector2 (0, 0);

		if (doClose) {
			movement = new Vector2 ((xdist / Mathf.Abs (xdist)) * speed.x, (ydist / Mathf.Abs (ydist)) * speed.y);
		} else {
			if (Mathf.Abs (xdist) > Mathf.Abs (ydist)) {
				if (ydist>0.5f)
					movement = new Vector2 (0, (ydist / Mathf.Abs (ydist)) * speed.y);
			} else {
				if (xdist>0.5f)
					movement = new Vector2 ((xdist / Mathf.Abs (xdist)) * speed.x, 0);
			}
		}

		if (movement.y > 0) {
			animator.speed = 1;
			animator.SetInteger ("Dir", 0);
		} else if (movement.y < 0) {
			animator.speed = 1;
			animator.SetInteger ("Dir", 2);
		} else if (movement.x > 0) {
			animator.speed = 1;
			animator.SetInteger ("Dir", 1);
		} else if (movement.x < 0) {
			animator.speed = 1;
			animator.SetInteger ("Dir", 3);
		} else {
			animator.speed = 0;
		}

		// Clamp enemy to screen
		
		var dist = (transform.position - Camera.main.transform.position).z;
		
		var leftBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0.1f, 0, dist)
			).x;
		
		var rightBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0.7f, 0, dist)
			).x;
		
		var topBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
			).y;
		
		var bottomBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 1, dist)
			).y;
		
		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
			Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
			transform.position.z
			);
		
		phys.velocity  = movement;

	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.GetComponent<ShotScript> () != null) {
			//splode.transform.position = transform.position;
			var newsplode = Instantiate(splode);
			newsplode.transform.position = transform.position;
			Destroy (newsplode.gameObject,splode.gameObject.GetComponent<ParticleSystem>().duration);
			Destroy (gameObject);
		}
	}
}
