using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentripetalForce : MonoBehaviour {

	public GameObject handle;
	float r;
	float m;
	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		m = rb.mass;
		r = Vector3.Distance (transform.position, handle.transform.position);
	}

	// Update is called once per frame
	void FixedUpdate () {
		//Debug.Log (rb.velocity);
		Vector3 a = rb.velocity;
		Vector3 b = (handle.transform.position - transform.position);
		Vector3 c = (Vector3.Dot (a, b) * (1 / b.magnitude)) * b;
		c = a - c;
		float cv = c.sqrMagnitude;
		float f = (m * cv) / r;
		//Debug.Log ((handle.transform.position - transform.position) * f);
		//Debug.DrawRay (transform.position, (handle.transform.position - transform.position));
		rb.AddForce ((handle.transform.position - transform.position) * f);
	}
}
