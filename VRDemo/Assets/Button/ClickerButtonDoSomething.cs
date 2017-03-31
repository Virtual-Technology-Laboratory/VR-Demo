using UnityEngine;
using System.Collections;

public class ClickerButtonDoSomething : MonoBehaviour{
	public Animator buttonAnimator;
	public GameObject thing;
	public Transform[] buttonPushers; 

	void OnTriggerEnter(){DoAThing ();}
	void Start (){InvokeRepeating ("CheckAndMaybeDo", 3, 1);}
	void CheckAndMaybeDo (){
		for (int i = 0; i < buttonPushers.Length; i++)
			if ((transform.position - buttonPushers [i].position).sqrMagnitude < .03f)
				DoAThing ();
	}
	void DoAThing (){
		buttonAnimator.SetTrigger ("DoSomething");
		var clone = Instantiate(thing, this.transform.position + Vector3.down*.2f, this.transform.rotation) as GameObject;
	}
}