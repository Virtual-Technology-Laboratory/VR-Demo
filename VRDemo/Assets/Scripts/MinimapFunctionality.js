#pragma strict
public var playerWorldCoordinates: Transform;
public var miniMapMarker: Transform;

function Update(){
	miniMapMarker.localPosition = playerWorldCoordinates.position;
}