#pragma strict

public  var characterAnimator: Animator;
private var rotationDamping : float = 2.0;
public  var followTarget : Transform;
public  var closeDistance : float = 3.0;
public  var delayTime : float = 3.0;
private var stateWalk : int = 1;
private var stateStop : int = 0;

function Update(){
			var offset = followTarget.position - transform.position;
			var offsetSquared = offset.sqrMagnitude;
			if( offsetSquared < closeDistance*closeDistance ){
				setState(stateStop);
			}
			else move();
}	

function setState(val : int){
	characterAnimator.SetInteger("State",val);
	/*		if(val==1){ sfxAudiosource.clip = sfxClips[sfxLocal]; sfxAudiosource.Play();}
	if(val==0){	sfxAudiosource.Stop();}		*/
}

function move(){
	var rot1 : Vector3 = followTarget.position - transform.position;
	rot1.y=0;
	var rot2 = Quaternion.LookRotation(rot1);
	transform.rotation = Quaternion.Slerp(transform.rotation, rot2, Time.deltaTime * rotationDamping);
	yield WaitForSeconds(delayTime);
	setState(stateWalk);
}