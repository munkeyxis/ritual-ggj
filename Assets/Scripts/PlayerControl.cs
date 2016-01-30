﻿using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public Vector2 speed = new Vector2(50,50);
	public GameObject shot;
	public Vector2 shotSpeed;
	public float shotInterval=1.0f;

	private Vector2 movement;
	private Rigidbody2D phys;
	private float nextShot=0;

	// Use this for initialization
	void Start () {
		phys = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
	
		nextShot += Time.deltaTime;

		float inputX = Input.GetAxis ("Horizontal");
		float inputY = Input.GetAxis ("Vertical");

		movement = new Vector2 (speed.x * inputX, speed.y * inputY);

		Vector2 shotDir = new Vector2(0,0);

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

		phys.AddForce (movement);


	}
}
