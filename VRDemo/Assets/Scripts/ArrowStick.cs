using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ArrowStick : MonoBehaviour {

	bool stuck = false;

	void OnCollisionEnter(Collision c) {
		ContactPoint[] cp = c.contacts;
		foreach (ContactPoint cpt in cp) {
			if (transform.InverseTransformPoint (cpt.point).z > 0) {
				transform.position += transform.forward * 0.1f;
				Destroy (GetComponent<Arrow> ().glintParticle);
				Destroy (GetComponent<Arrow>().arrowHeadRB.GetComponent<Collider>());
				Destroy (GetComponent<Collider> ());
				GetComponent<Rigidbody> ().isKinematic = true;
				GetComponent<Arrow> ().enabled = false;
				Destroy (GetComponent<Rigidbody> ());
				transform.parent = c.gameObject.transform;


				stuck = true;
				return;
			}
		}
	}
}
