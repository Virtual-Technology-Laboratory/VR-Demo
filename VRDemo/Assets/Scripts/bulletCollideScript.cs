using UnityEngine;

public class bulletCollideScript : MonoBehaviour {

	void OnCollisionEnter (Collision c){
		Rigidbody r = GetComponent<Rigidbody> ();
		r.useGravity = true;

		foreach (Transform child in transform) {
			Destroy(child.gameObject);
		}
		GameObject p1 = GameObject.FindGameObjectWithTag ("Player");
		if ((transform.position - p1.transform.position).sqrMagnitude > 40f) {
			Destroy (GetComponent<Rigidbody> ());
			Destroy (GetComponent<Collider> ());
			Destroy (gameObject, 1);
			transform.parent = c.gameObject.transform;
		}
	}
}