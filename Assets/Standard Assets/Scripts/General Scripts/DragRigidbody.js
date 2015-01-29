//Edited from Unity's example script

var spring = 50.0;
var damper = 5.0;
var drag = 10.0;
var angularDrag = 5.0;
var distance = 0.2;
var attachToCenterOfMass = false;
var raycastMask : LayerMask;
var hitObject : GameObject;
var hitPoint : Vector3;
private var springJoint : SpringJoint;

function Update ()
{
	// Make sure the user pressed the mouse down
	

	var mainCamera = FindCamera();
	hitObject = null;
	// We need to actually hit an object
	var hit : RaycastHit;
	if (!Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition),  hit, 100, raycastMask))
		return;
	
	
	// We need to hit a rigidbody that is not kinematic
	if (!hit.rigidbody || hit.rigidbody.isKinematic)
		return;
	else{
		hitObject = hit.collider.gameObject;
		hitPoint = hit.point;
	}
	
	if (!Input.GetMouseButtonDown (0))
		return;
	
	if (!springJoint)
	{
		var go = new GameObject("Rigidbody dragger");
		var body : Rigidbody = go.AddComponent ("Rigidbody") as Rigidbody;
		springJoint = go.AddComponent ("SpringJoint");
		body.isKinematic = true;
	}
	var hitpos : Vector3 = hit.point;
	hitpos.z = 0;
	springJoint.transform.position = hitpos;// hit.point;
	hit.rigidbody.transform.position.z = 0;
	if (attachToCenterOfMass)
	{
		var anchor = transform.TransformDirection(hit.rigidbody.centerOfMass) + hit.rigidbody.transform.position;
		anchor = springJoint.transform.InverseTransformPoint(anchor);
		springJoint.anchor = anchor;
	}
	else if(springJoint && springJoint.connectedBody)
	{
		springJoint.anchor = Vector3.zero;
	}
	
	springJoint.spring = spring;
	springJoint.damper = damper;
	springJoint.maxDistance = distance;
	springJoint.connectedBody = hit.rigidbody;
	
	StartCoroutine ("DragObject", hit.distance);
}

function DragObject (distance : float)
{
	var oldDrag = springJoint.connectedBody.drag;
	var oldAngularDrag = springJoint.connectedBody.angularDrag;
	springJoint.connectedBody.drag = drag;
	springJoint.connectedBody.angularDrag = angularDrag;
	var mainCamera = FindCamera();
	while (Input.GetMouseButton (0) && springJoint.connectedBody && springJoint.connectedBody.gameObject)
	{
		var ray = mainCamera.ScreenPointToRay (Input.mousePosition);
		springJoint.transform.position = ray.GetPoint(distance);
		hitObject = springJoint.connectedBody.gameObject;
		yield;
	}
	if (springJoint.connectedBody)
	{
		springJoint.connectedBody.drag = oldDrag;
		springJoint.connectedBody.angularDrag = oldAngularDrag;
		springJoint.connectedBody = null;
		hitObject = null;
	}
}

function FindCamera ()
{
	if (GetComponent.<Camera>())
		return GetComponent.<Camera>();
	else
		return Camera.main;
}