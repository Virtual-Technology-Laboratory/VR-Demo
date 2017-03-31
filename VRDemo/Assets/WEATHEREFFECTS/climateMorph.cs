using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class climateMorph : MonoBehaviour {

	[Range(0,1)]
	public float temperature;
	[Range(0,1)]
	public float humidity;
	[Range(0,1)]
	public float evilness;

	[Range(0,1)]
	public float snowCoverage;


	public Material snowCoverMat;

	[Header("Precipitation types")]
	public ParticleSystem rain;
	public float rainMax;
	public ParticleSystem snow;
	public float snowMax;

	// Use this for initialization
	void Start () {
		disablePrecipitation ();
	}
	
	// Update is called once per frame
	void Update () {
		
		Precipitation ();
		snowCoverMat.SetFloat("_SnowLevel", Mathf.Lerp(1, -1, snowCoverage));
	}

	void Precipitation()
	{
		//If the humidity is high enough, precipitate the water out of the atmosphere
		if (humidity >= 0.5f) { 
			ParticleSystem.EmissionModule mod;
			if (temperature >= 0.2f) {
				mod = rain.emission;
				mod.enabled = true;
				mod.rateOverTime = Mathf.Lerp(0, rainMax, (humidity - 0.5f) * 2);
				snow.Clear ();
				snowCoverage -= Time.deltaTime * temperature;
			} else {
				mod = snow.emission;
				mod.enabled = true;
				mod.rateOverTime = Mathf.Lerp(0, snowMax, (humidity - 0.5f));
				ParticleSystem.MainModule mainMod = snow.main;
				mainMod.startSpeed = (Mathf.Lerp (1, 20, evilness));
				rain.Clear ();
				snowCoverage += Time.deltaTime * evilness * 0.01f;
			}
		} else { // Otherwise, taper off the faucet.
			disablePrecipitation();
			snowCoverage -= Time.deltaTime * temperature;
		}


		// Clamp Snow
		snowCoverage = Mathf.Clamp(snowCoverage, 0, 2);
	}

	void disablePrecipitation()
	{
		ParticleSystem.EmissionModule mod;
		mod = rain.emission;
		mod.enabled = false;
		mod = snow.emission;
		mod.enabled = false;
	}
}
