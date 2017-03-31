#pragma strict
@script RequireComponent(Light)
 
 var minIntensity : float = 0.25f;
 var maxIntensity : float = 0.5f;
 var flickerRate : float = 1;
 
 private var random : float;  
 
 random = Random.Range(0.0f, 65535.0f);
 
 function Update()
 {
     var noise = Mathf.PerlinNoise(random, flickerRate * Time.time);
     GetComponent.<Light>().intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
 }