﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public GameObject DeadScreen;

	private float speed = 0;
	private float minSpeed = 0;
	private float maxSpeed = 10f;
	private float acceleration = 0.25f;
	private float deceleration = 0.1f;
	private Rigidbody rbody;
	private Quaternion lastTouchPadOrientation;

	void Start() {
		rbody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {
		// Move only forward
//		Vector3 targetMoveVector = ori * (Vector3.forward * Time.deltaTime * 1.5f);
		
		// Move in opposite direction
		// Vector3 targetMoveVector = ori * - (Vector3.forward * Time.deltaTime * 1.5f);

		if (GvrControllerInput.AppButton) {
			DeadScreen.SetActive(false);
		}
		
		// rbody.velocity = movement * speed;
		if (GvrControllerInput.ClickButton) { // Speed up by clicking on TouchPad
			lastTouchPadOrientation = GvrControllerInput.Orientation;
			speed = Mathf.Min(speed + acceleration, maxSpeed);
		} else if (speed > minSpeed) { // Slow down (TouchPad was released
			speed = Mathf.Max(speed - deceleration, minSpeed);
		}
		
		if (speed > 0) {
			Vector3 targetMoveVector = lastTouchPadOrientation * (Vector3.forward * Time.deltaTime * (1 + speed));
			targetMoveVector.y = 0;
			transform.position += targetMoveVector;			
		}
//		rbody.position += targetMoveVector;
	}

	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Asteroid")) {
			// Destroy(gameObject);
			DeadScreen.SetActive(true);
		}
	}
}
