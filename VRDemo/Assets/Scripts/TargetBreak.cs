using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBreak : MonoBehaviour {
	void OnCollisionEnter(Collision c) {
		if (c.gameObject.tag == "projectile") {

			// This is where Thom added stuff. If it breaks check here first :D
			if (transform.parent != null && transform.parent.childCount < 3) {
				for (int i = 0; i < transform.parent.childCount; i++) {
					GameObject thing = transform.parent.GetChild (i).gameObject;
					Destroy(thing,10);
					thing.GetComponent<Rigidbody> ().isKinematic = false;
					thing.transform.parent = null;
					return;
				}
			}

			//Destroy (c.gameObject);
			Destroy(gameObject,10);
			GetComponent<Rigidbody> ().isKinematic = false;
			gameObject.layer = 4;
			transform.parent = null;

			GetComponent<Rigidbody> ().AddForceAtPosition (c.contacts [0].normal * Random.Range(100, 200), c.contacts [0].point);

		}
	}
}
