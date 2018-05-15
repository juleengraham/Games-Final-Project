using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private float sp;
	public GameObject player;
	private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		sp = 2;
	}
	
	// Update is called once per frame
	/*void Update () {
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			Vector3 pos = this.transform.position;
			pos.x--;
			this.transform.position = pos;
		}
	}*/

	void FixedUpdate () {
		float dx = 0f;
		float dy = 0f;
		float dz = 0f;
		bool up = Input.GetKey ("w") || Input.GetKey ("up");
		bool down = Input.GetKey ("s") || Input.GetKey ("down");
		bool left = Input.GetKey ("a") || Input.GetKey ("left");
		bool right = Input.GetKey ("d") || Input.GetKey ("right");

		if (up) {
			dy = sp;
		} else if (down) {
			dy = -sp;
		} else if (left) {
			dx = - sp;
		} else if (right) {
			dx = sp;
		}

		rigidBody.velocity = new Vector3 (dx, dy, dz);

		/*

		//Store the current horizontal input in the float moveHorizontal.
		float moveHorizontal = Input.GetAxis("Horizontal");

		//Store the current vertical input in the float moveVertical.
		float moveVertical = Input.GetAxis("Vertical");

		//Use the two store floats to create a new Vector2 variable movement.
		Vector2 movement = new Vector2(moveHorizontal, moveVertical);

		//Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
		rigidBody.AddForce(movement * sp);*/
	}
}
