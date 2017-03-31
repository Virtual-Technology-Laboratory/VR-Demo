#pragma strict
public var particleDust: ParticleSystem;
public var kickUpDust: boolean = false;
public var distanceFromGround: float = 0.1;
public var moveMaxSpeed: float = 60.0;
public var moveAcceleration: float = 0.5;
private var gravity: float = 9.8;
protected var character: Rigidbody;
private var stopped: boolean = false;
public var backupWhenStopped: boolean = true;
private var stopCheckTimer: float = 0;
public var rotationDamping: float = 1;
private var distanceFromTarget: float;
private var target: Vector3;
private var closeEnough: float = 10.0f;
private var waypointIndex: int;
public var waypoints: Transform[];

@script RequireComponent(Rigidbody)

function Awake (){
	character = GetComponent.<Rigidbody>();
	character.useGravity = false;
	character.freezeRotation = true;
}
function Start(){
	var Index = UnityEngine.Random.Range(0,(waypoints.length)-1);
	waypointIndex = parseInt(Index);
	target = waypoints[waypointIndex].position;
    InvokeRepeating ("SpeedCheck", 3, 0.5); //starting in 3 seconds, check speed every 0.5 seconds.
    InvokeRepeating ("StopBackingUp", 3, 1.5); //starting in 3 seconds, make sure things are moving forwards every 1.5 seconds.
}

function Update (){
//find new waypoint
    distanceFromTarget = Vector3.Distance(target, transform.position);
    if (distanceFromTarget < closeEnough || stopped){
    		stopped = false;
    		stopCheckTimer = 0.0;
		var Index = UnityEngine.Random.Range(0,(waypoints.length)-1);
		waypointIndex = parseInt(Index);
		target = waypoints[waypointIndex].position;
	}
//  Kick up dust
	if (kickUpDust) {
		if (!IsGrounded() && particleDust.isPlaying) particleDust.Stop();
		else if (!particleDust.isPlaying) particleDust.Play();
	}
//move
	var targetVelocity = new Vector3(0, 0, 0);
	targetVelocity += transform.forward * moveMaxSpeed;
	var velocityChange = targetVelocity - character.velocity;
	velocityChange.x = Mathf.Clamp(velocityChange.x, -moveAcceleration, moveAcceleration);
	velocityChange.z = Mathf.Clamp(velocityChange.z, -moveAcceleration, moveAcceleration);
	velocityChange.y = 0;
	character.AddForce(velocityChange, ForceMode.VelocityChange);
//gravity
	character.AddForce(Vector3 (0, -gravity * character.mass, 0));
//look toward next waypoint
	var rotationGoal = Quaternion.LookRotation(target - transform.position, Vector3.up);
	rotationGoal.x = 0.0;
    rotationGoal.z = 0.0;
	transform.rotation = Quaternion.Slerp(transform.rotation, rotationGoal, Time.deltaTime * rotationDamping);
	//transform.LookAt(target);
}

//currently called every 0.5 seconds.
function SpeedCheck() {
	if (Mathf.Abs(character.velocity.x + character.velocity.z) < 0.02){
		stopCheckTimer += 1.0;
		if (stopCheckTimer > 5.0){
			stopped = true;
			if (backupWhenStopped) moveMaxSpeed *= -1.0;
			//Debug.Log(name +" is Stopped");
		}
	}
	else {
		stopped = false;
		stopCheckTimer = 0.0;
	}
}

function IsGrounded(): boolean {
	var hit : RaycastHit;
	var distanceToGround: float;
	if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.down) , hit, Mathf.Infinity))
		distanceToGround = hit.distance;
 	return !(distanceToGround > distanceFromGround);
}

function StopBackingUp() {
	if (moveMaxSpeed < 0.0) moveMaxSpeed *= -1.0;
}