using UnityEngine;
using System.Collections;

public class TouchEnabler : MonoBehaviour {
	public Collider[] collidersToEnable;


	public GameObject[] objectsToHide;
	public Rigidbody[] rigidbodiesToEnable;
	//public LayerMask[] defaultLayerMasks;
	void Start(){
		/*
		if(!objectsToHide){
			Debug.LogError("missing object to unhide");
		}*/
		//defaultLayerMasks = new LayerMask[rigidbodiesToEnable.Length];
		/*for (int i = 0; i < rigidbodiesToEnable.Length; i++) {
			defaultLayerMasks[i] = rigidbodiesToEnable[i].gameObject.layer;
			rigidbodiesToEnable[i].gameObject.layer = LayerMask.NameToLayer("RaycastOnly");
		}*/
		print ("jfdasfaspdf");
	}
	void OnMouseDown() {
		print ("TOUCH");
		Unhide();
		EnableColliders ();
		EnableRigidbodies ();
	}
	
	void Unhide(){
		for (int  i = 0; i < objectsToHide.Length; i++) {
			objectsToHide[i].renderer.enabled = false;
		}
		//objectToUnhide.renderer.enabled = false;
	}
	void EnableColliders(){
		for (int  i = 0; i < collidersToEnable.Length; i++) {
			collidersToEnable[i].collider.enabled = true;
		}
	}
	void EnableRigidbodies(){

		for (int  i = 0; i < rigidbodiesToEnable.Length; i++) {
			print (rigidbodiesToEnable [i]);
			rigidbodiesToEnable[i].isKinematic = false;
			//rigidbodiesToEnable[i].gameObject.layer = defaultLayerMasks[i];
			//rigidbodiesToEnable[i].useGravity = true;
		}
	}
}
