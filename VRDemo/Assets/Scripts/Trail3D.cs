using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using System.Collections.Generic;

public class Trail3D : MonoBehaviour {

	Player player;
	public GameObject tubeRenderer;
	public Transform startPoint;
	GameObject tr;
	static List<GameObject> oldTube = new List<GameObject>();
	Vector3 lastPoint;
	float lastTime;
	public float dist = 0.05f;
	Hand hand;

	// Use this for initialization
	void Start () {
		player = Player.instance;
		hand = GetComponentInParent<Hand> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (tr == null && hand.controller != null && hand.controller.GetPressDown (Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger)) {
			tr = Instantiate (tubeRenderer, startPoint.position, Quaternion.identity) as GameObject;
			lastPoint = startPoint.position;
			lastTime = Time.time;
		}
		if (tr != null && hand.controller != null && hand.controller.GetPressUp (Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger)) {
			tr.GetComponent<TubeRenderer> ().AddPoint (startPoint.position, 0);
			oldTube.Add (tr);
			tr = null;
		}
		if (hand.controller != null && hand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip)) {
			if (oldTube.Count > 0) {
				foreach (GameObject o in oldTube) {
					Destroy (o);
				}
				oldTube.Clear ();
			}
		}

		if (tr != null) {
			if (Vector3.Distance (startPoint.position, lastPoint) > dist) {
				float rad = 0.06f;
				if (Time.time - lastTime < 1) {
					rad = Mathf.Pow((Time.time - lastTime),.8f) * 0.06f;
				}
				tr.GetComponent<TubeRenderer> ().AddPoint (startPoint.position, rad);
				lastPoint = startPoint.position;
				lastTime = Time.time;
			}
		}
	}
}
