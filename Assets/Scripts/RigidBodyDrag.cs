using UnityEngine;
using System.Collections;

public class RigidBodyDrag : MonoBehaviour {
	//Edited from Unity's example script
	
	public float spring = 50.0f;
	public float damper = 5.0f;
	public float drag = 10.0f;
	public float angularDrag = 5.0f;
	public float distance = 0.2f;
	public bool attachToCenterOfMass = false;
	float hitdst;
	private SpringJoint springJoint;
	
	void Update ()
	{
		if(Input.GetMouseButtonUp (0) && springJoint && springJoint.connectedBody){
			//springJoint.connectedBody.drag = oldDrag;
			//springJoint.connectedBody.angularDrag = oldAngularDrag;
			springJoint.connectedBody = null;
		}
		if(springJoint && springJoint.connectedBody != null){
			DragObject(hitdst);
		}
		// Make sure the user pressed the mouse down
		if (!Input.GetMouseButtonDown (0)){

			return;
		}
		
		var mainCamera = FindCamera();
		
		// We need to actually hit an object
		RaycastHit hit;
		if (!Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 100))
			return;
		// We need to hit a rigidbody that is not kinematic
		if (!hit.rigidbody || hit.rigidbody.isKinematic)
			return;


		
		if (!springJoint)
		{
			GameObject go = new GameObject("Rigidbody dragger");
			Rigidbody body = go.AddComponent ("Rigidbody") as Rigidbody;
			springJoint = go.AddComponent ("SpringJoint") as SpringJoint;
			body.isKinematic = true;
		}
		
		springJoint.transform.position = hit.point;
		if (attachToCenterOfMass)
		{
			var anchor = transform.TransformDirection(hit.rigidbody.centerOfMass) + hit.rigidbody.transform.position;
			anchor = springJoint.transform.InverseTransformPoint(anchor);
			springJoint.anchor = anchor;
		}
		else
		{
			springJoint.anchor = Vector3.zero;
		}

		springJoint.spring = spring;
		springJoint.damper = damper;
		springJoint.maxDistance = distance;
		springJoint.connectedBody = hit.rigidbody;
		hitdst = hit.distance;

		//StartCoroutine ("DragObject", hit.distance);
	}

	void DragObject (float distance)
	{
		//var oldDrag = springJoint.connectedBody.drag;
		//var oldAngularDrag = springJoint.connectedBody.angularDrag;
		springJoint.connectedBody.drag = drag;
		springJoint.connectedBody.angularDrag = angularDrag;
		var mainCamera = FindCamera();
		//while (Input.GetMouseButton (0))
		//if (Input.GetMouseButton (0))
		//{
			Ray ray = mainCamera.ScreenPointToRay (Input.mousePosition);
			springJoint.transform.position = ray.GetPoint(distance);
			//yield;
		//}

	}
	
	Camera FindCamera ()
	{
		if (GetComponent<Camera>())
			return GetComponent<Camera>();
		else
			return Camera.main;
	}
}