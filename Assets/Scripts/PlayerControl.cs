﻿using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public Vector2 speed = new Vector2(50,50);
	public GameObject shot;
	public Vector2 shotSpeed;
	public float shotInterval=1.0f;
	public GameObject splode;

	private Vector2 movement;
	private Rigidbody2D phys;
	private float nextShot=0;
	private Animator animator;

	public int HP=100;
	public int shotDamage=25;

	public static PlayerControl instance;

	// Use this for initialization
	void Start () {
		phys = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		instance = this;
	}

	// Update is called once per frame
	void Update () {
	
		nextShot += Time.deltaTime;

		// Automatically detects WASD
		float inputX = Input.GetAxis ("Horizontal");
		float inputY = Input.GetAxis ("Vertical");

		movement = new Vector2 (speed.x * inputX, speed.y * inputY);

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

		//else {
		//	animator.SetInteger ("Dir", 5);
		//}
			
			Vector2 shotDir = new Vector2(0,0);

		// Check shot keys
		if (Input.GetKey (KeyCode.I)) {
			shotDir = new Vector2(0,shotSpeed.y);
		} else if (Input.GetKey (KeyCode.J)) {
			shotDir = new Vector2(-shotSpeed.x,0);
		} else if (Input.GetKey (KeyCode.K)) {
			shotDir = new Vector2(0,-shotSpeed.y);
		} else if (Input.GetKey (KeyCode.L)) {
			shotDir = new Vector2(shotSpeed.x,0);
		}

		if (shotDir != new Vector2(0,0) && nextShot>shotInterval) {
			nextShot = 0;
			// SoundEffectsScript.Instance.PlayShot();
			GameObject newShot = Instantiate (shot) as GameObject;
			Rigidbody2D shotPhys = newShot.GetComponent<Rigidbody2D> ();
			Destroy (newShot,3);
			shotPhys.velocity = shotDir;
			newShot.transform.position = new Vector2(transform.position.x + shotDir.x/shotSpeed.x,transform.position.y+shotDir.y/shotSpeed.y);
			//SFX
		}

		// Clamp player to screen

		var dist = (transform.position - Camera.main.transform.position).z;

		var leftBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0.05f, 0, dist)
			).x;
		
		var rightBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0.95f, 0, dist)
			).x;
		
		var topBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0.07f, dist)
			).y;
		
		var bottomBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0.9f, dist)
			).y;
		
		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
			Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
			transform.position.z
			);

		// This actually does the moving
		phys.velocity = movement;


	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.GetComponent<ShotScript> () != null) {
			HP-= PlayerControl.instance.shotDamage;
			if (HP<=0)
			{
				UITracker.instance.SetP1Health(0);
				//splode.transform.position = transform.position;
				var newsplode = Instantiate(splode);
				newsplode.transform.position = transform.position;
				Destroy (newsplode.gameObject,splode.gameObject.GetComponent<ParticleSystem>().duration);
				Destroy (gameObject);
				UITracker.instance.EndLevel(false);
			}
			else
			{
				UITracker.instance.SetP1Health(HP);
			}
		}
	}

}
