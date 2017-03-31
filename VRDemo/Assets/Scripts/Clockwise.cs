using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clockwise : MonoBehaviour {
    public int Speed;


	
	// Update is called once per frame
	void Update () {

        transform.Rotate(0, 0, Time.deltaTime * Speed);
		
	}
}
