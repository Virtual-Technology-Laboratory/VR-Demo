using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem{
	public class gunShootStuff : MonoBehaviour {
		private Player player;
		Hand hand;
		public Transform muzzle;
		public GameObject bullet;


		void Start () {
			player = InteractionSystem.Player.instance;
			hand = GetComponentInParent<Hand> ();
		}

		void Update () {
			if (hand.controller != null && hand.controller.GetPressDown (SteamVR_Controller.ButtonMask.Trigger)) {
				Shoot();
			}
		}

		void Shoot() {
			var clone = Instantiate(bullet, muzzle.position, muzzle.rotation) as GameObject;
			clone.GetComponent<Rigidbody>().AddForce(muzzle.forward * 2500);
			Destroy(clone.gameObject, 10);
		}
	}
}