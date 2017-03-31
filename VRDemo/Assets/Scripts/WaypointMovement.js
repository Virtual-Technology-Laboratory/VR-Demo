#pragma strict
import UnityEngine.SceneManagement;

private var target : Transform;
public var closeEnough : float = 2.0f;
public var moveSpeed : float = 2.0;
public var rotationDamping = 2.0;
public var waypoints : Transform[];
	     
function Start() {
	InvokeRepeating("GetTarget",0,4);
}
	     
function Update () {
    var distance = Vector3.Distance(target.position, transform.position);
	move();
    if (distance < closeEnough) GetTarget();
}

function GetTarget() {
	var i = UnityEngine.Random.Range(0,(waypoints.length)-1);
	target = waypoints[i];
}

function move() {
	var rotGoal = Quaternion.LookRotation(target.position - transform.position);//rotGoal.y=0;
	transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, Time.deltaTime * rotationDamping);
	transform.Translate(Vector3(0,0,moveSpeed) * Time.deltaTime);
}