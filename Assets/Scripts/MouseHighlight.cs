using UnityEngine;
using System.Collections;

public class MouseHighlight : MonoBehaviour {
	public GameObject mouseHighlightObject;
	public DragRigidbody drag;
	// Use this for initialization
	void Start () {
		drag = FindObjectOfType<DragRigidbody>();

	}
	
	// Update is called once per frame
	void Update () {

		if(!drag){
			Debug.LogError("no Dragrigidbody component found in scene");
		}
		if(drag.hitObject){
			mouseHighlightObject.renderer.enabled = true;
		}
		else{
			mouseHighlightObject.renderer.enabled = false;
		}
		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.z = mouseHighlightObject.transform.position.z;
		mouseHighlightObject.transform.position = pos;
	}
}
