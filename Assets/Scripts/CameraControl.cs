using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	Vector3 lastpos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(1)){
			lastpos=Input.mousePosition;
		}
		if(Input.GetMouseButton(1)){
			Vector3 delta = Input.mousePosition - lastpos;
			transform.position -= delta * 0.02f;
			lastpos=Input.mousePosition;
		}
	}
}
