using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomizeColor : MonoBehaviour{

	public Transform[] bones;
	public GameObject[] extraLimbs;
	public Renderer[] moarParts;
	Renderer rend;
	SkinnedMeshRenderer blendRenderer;

	void Start (){
		rend = GetComponent<Renderer> ();
		blendRenderer = GetComponent<SkinnedMeshRenderer> ();
		GetRandomMonster();
	}
	void OnTriggerEnter(Collider c) {
		//Debug.Log (name + " was hit!");
		if (c.gameObject.tag == "projectile"){
			GetRandomMonster ();
			Destroy (c.gameObject);
		}
	}
	void GetRandomMonster (){
		SetTints ();
		for (int i=0; i<moarParts.Length; i++){
			moarParts[i].material = rend.material;
		}
		ResizeBonesSymetric ();
		setBlendWeights ();
		getExtraLimbs ();
	}
	void SetTints (){
		rend.material.SetColor("_Color1", GetAColor());
		rend.material.SetColor("_Color2", GetAColor());
		rend.material.SetColor("_Color3", GetAColor());
		rend.material.SetColor("_Color4", GetABrightColor());
		rend.material.SetFloat("_Map2Str", Random.Range(0f,1f));
	}
	Color GetAColor (){
		float h = Mathf.Pow(Random.Range(0f,0.82f),2);					//prefer orange & never go purple
		float s = Mathf.Pow(Random.Range(0f,1f),2);						//prefer low saturations
		//float v = Mathf.Tan(Random.Range(-1f,1.4f))/7.3553f+.21174f;	//prefer mid-low values
		float v = Random.Range(0f,1f);
		return Color.HSVToRGB(h,s,v);
	}
	Color GetABrightColor (){
		return  new Color(Random.Range(0f,1f),Random.Range(0f,1f),Mathf.Pow(Random.Range(0f,1f),2),1f);
	}
	void ResizeBonesSymetric (){
		for (int i=0; i<bones.Length-1; i+=2){
			var a = Random.Range(0.5f,1.5f);
			bones [i].localScale = bones [i+1].localScale= new Vector3 (a, a, a);
		}
	}
	void setBlendWeights (){
		for (int i = 0; i < blendRenderer.sharedMesh.blendShapeCount; i++) {
			blendRenderer.SetBlendShapeWeight(i,Random.Range(0f,100f));
		}
	}
	void getExtraLimbs (){
		for (int i = 0; i<extraLimbs.Length; i++) {
			extraLimbs [i].SetActive (randomBool ());
		}
	}
	bool randomBool (){
		return (Random.value > 0.5f);
	}
}