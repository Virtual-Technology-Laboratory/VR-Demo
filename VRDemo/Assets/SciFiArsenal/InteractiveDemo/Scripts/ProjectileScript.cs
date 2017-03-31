using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour 
{
    public GameObject impactParticle;
    public GameObject projectileParticle;
    public GameObject[] trailParticles;
    [HideInInspector]
    public Vector3 impactNormal; //Used to rotate impactparticle.

	public Color myColor;

	// FIXME quick fix to prevent self collision HACK
	private float count;
	
	void Start () 
	{
        projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation) as GameObject;
        projectileParticle.transform.parent = transform;
		count = 0;
		Destroy (gameObject, 20);
		GetComponent<SphereCollider> ().enabled = false;
		//colorize ();
	}

	void Update ()
	{
		count += Time.deltaTime;
//		Debug.Log (count);
		if (count >= 0.03f) 
		{
			GetComponent<SphereCollider> ().enabled = true;
		}
	}

	void OnCollisionEnter (Collision hit) {
		//Debug.Log(hit.collider + ", on " + hit.gameObject.name);

        //transform.DetachChildren();
        impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;
		Light[] lights = impactParticle.GetComponentsInChildren<Light> ();
		for (int i = 0; i < lights.Length; i++) 
		{
			if (myColor != Color.blue) {
				lights [i].color = myColor;
			}
		}
		//colorize();
        //Debug.DrawRay(hit.contacts[0].point, hit.contacts[0].normal * 1, Color.yellow);

        if (hit.gameObject.tag == "Destructible") // Projectile will destroy objects tagged as Destructible
        {
            Destroy(hit.gameObject);
        }


        //yield WaitForSeconds (0.05);
        foreach (GameObject trail in trailParticles)
	    {
			if(transform.Find(projectileParticle.name + "/" + trail.name) == null) {continue;}
			GameObject curTrail = transform.Find(projectileParticle.name + "/" + trail.name).gameObject;
            curTrail.transform.parent = null;
            Destroy(curTrail, 3f); 
	    }
        Destroy(projectileParticle, 3f);
        Destroy(impactParticle, 5f);
        Destroy(gameObject);
        //projectileParticle.Stop();

	}

}