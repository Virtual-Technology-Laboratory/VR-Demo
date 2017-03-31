using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnCollision : MonoBehaviour {

//	Rigidbody rb;
	AudioSource sound;

	void Start () {
//		rb = GetComponent<Rigidbody> ();
		sound = GetComponent<AudioSource> ();
	}
	void OnCollisionEnter (){
		sound.Play ();
		Debug.Log (name + " is playing.");
	}
}
