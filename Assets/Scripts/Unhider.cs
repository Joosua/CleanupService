using UnityEngine;
using System.Collections;

public class Unhider : MonoBehaviour {

	public GameObject objectToUnhide;

	void Start(){
		/*
		if(!objectToUnhide){
			Debug.LogError("missing object to unhide");
		}*/

	}
	void OnMouseDown() {
		print ("JEPJEPJEPJEPJEPJEP");
		//Unhide();
	}

	void Unhide(){
		objectToUnhide.renderer.enabled = false;
	}
}
