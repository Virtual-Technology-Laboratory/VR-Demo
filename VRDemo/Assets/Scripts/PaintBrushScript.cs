using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem {
	
	public class PaintBrushScript : MonoBehaviour {

		private Player player; 
		public ParticleSystem PaintTrail;

		void Start () {
			PaintTrail.Stop ();
			player = InteractionSystem.Player.instance;
		}
		void Update () {
			foreach (Hand hand in player.hands) {
				if (hand.controller != null && hand.controller.GetPressDown (SteamVR_Controller.ButtonMask.Trigger)) PaintTrail.Play ();
				if (hand.controller != null && hand.controller.GetPressUp (SteamVR_Controller.ButtonMask.Trigger)) PaintTrail.Stop ();
			}

		}
	}
}
