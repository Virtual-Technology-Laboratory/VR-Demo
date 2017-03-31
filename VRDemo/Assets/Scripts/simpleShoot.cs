using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleShoot : MonoBehaviour {

	//Video
	public GameObject controllerLR;

	public SteamVR_TrackedObject trackedObject;
	private SteamVR_Controller.Device device;

	private SteamVR_TrackedController trackedCont;

	public Transform shootFrom;

	public GameObject projectile;
	public GameObject muzzleFlash;

	public float speed;



	// Use this for initialization
	void Start () {
		trackedCont = controllerLR.GetComponent<SteamVR_TrackedController> ();
		trackedCont.TriggerClicked += TriggerPressed;
		trackedObject = controllerLR.GetComponent<SteamVR_TrackedObject>();

		//InvokeRepeating ("Shoot", 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
//		if (SteamVR_Controller.Input(2).GetHairTriggerDown()) {
//			Shoot ();
//			Debug.Log("pewwww");
//		}
	}


	private void TriggerPressed(object sender, ClickedEventArgs e)
	{
		Debug.Log ("TriggerPressed WaitForSeconds called");
		Shoot ();
	}

	public void Shoot()
	{
		device = SteamVR_Controller.Input ((int)trackedObject.index);
		device.TriggerHapticPulse (750);
		Debug.Log ("Pew");
		GameObject newPew = Instantiate(projectile, shootFrom.position, shootFrom.rotation) as GameObject;
		GameObject newFlash = Instantiate(muzzleFlash, shootFrom.position, shootFrom.rotation) as GameObject;
		newFlash.transform.Rotate (0, 180, 0);
		newFlash.transform.localScale = Vector3.one * 0.4f;
		Destroy (newFlash, 2);
		newPew.GetComponent<Rigidbody>().AddForce (newPew.transform.forward * speed);
		//projectile.GetComponent<ProjectileScript>().impactNormal = hit.normal;
	}

}
