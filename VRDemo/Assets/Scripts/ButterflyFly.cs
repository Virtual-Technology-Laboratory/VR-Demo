using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyFly : MonoBehaviour {

	public float MoveSpeed;

	public float xFreq = 0.5f;  // Speed of sine movement
	public float xMag = 0.5f;   // Size of sine movement
	public float yFreq = 1f;  // Speed of sine movement
	public float yMag = 1f;   // Size of sine movement
	public float variation = 0.5f;
	public float TimePassed;
	public Transform[] waypoint;
	int waypointIndex;
	public Rigidbody rb;
	Quaternion originalRotation;

	int coefficient = 1;
	float lastPos;

	private Vector3 pos;

	void Start () {
		pos = transform.position;

		rb = GetComponent<Rigidbody> ();
		originalRotation = transform.rotation;
		waypointIndex = Random.Range (0, waypoint.Length);
	}

	void Update() {
		TimePassed += Time.deltaTime;
		if (TimePassed > 20) {
			TimePassed = 0;
			waypointIndex = Random.Range (0, waypoint.Length);
		}
		if (Vector3.Distance (transform.position, waypoint [waypointIndex].position) < .5f) {
			waypointIndex = Random.Range (0, waypoint.Length);
		}
	}

	void FixedUpdate () {
		float verticalSine = (yMag + Random.Range(0f, variation)) * Mathf.Sin (Time.fixedTime * yFreq);
		float horizontalSine = (xMag + Random.Range(0f, variation)) * Mathf.Sin (Time.fixedTime * xMag);
		rb.velocity = new Vector3(horizontalSine, verticalSine, rb.velocity.z);
		rb.AddForce (transform.forward * MoveSpeed);
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(waypoint[waypointIndex].position -  transform.position), 0.01f);

	}
}
