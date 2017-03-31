using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class MinimapFunctionalityC : MonoBehaviour {

	public Transform[] inWorldObject; //keep these arrays the same length!!!!!
	public Transform[] mapMarker;

	void Update(){
		for (int i = 0; i < inWorldObject.Length; i++) {
			mapMarker[i].localPosition = inWorldObject[i].position;
			mapMarker[i].localPosition = new Vector3 (mapMarker[i].localPosition.x, 0.01f, mapMarker[i].localPosition.z);
		}
	}
}