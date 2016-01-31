using UnityEngine;
using System.Collections;

public class FlyOnScript : MonoBehaviour {

	public Transform target;
	bool hitCenter;
	bool flyOff;
	double waitTime;
	public string nextLevel;
	public int driftSpeed = 0;
	
	// Use this for initialization
	void Start () {
		waitTime = 2;
	}
	
	// Update is called once per frame
	void Update () {
		if (hitCenter && !flyOff)
		{
			waitTime -= Time.deltaTime;
		}
		if (waitTime<=0)
		{
			flyOff = true;
		}
		if (flyOff && transform.position.x > 30)
		{
			if (!nextLevel.Equals(""))
			{
				Application.LoadLevel(nextLevel);
			}
			Destroy (transform.gameObject);
		}
	}
	
	void FixedUpdate() {
		if (transform.position.x < target.position.x)
		{
			(transform.GetComponent<Rigidbody2D>() as Rigidbody2D).velocity = new Vector2(50,0);
		}
		else if (hitCenter && flyOff)
		{
			(transform.GetComponent<Rigidbody2D>() as Rigidbody2D).AddForce(new Vector2(50,0));
		}
		else if (!hitCenter)
		{
			hitCenter = true;
			(transform.GetComponent<Rigidbody2D>() as Rigidbody2D).velocity=new Vector2(driftSpeed,0);
		}
	}
}
