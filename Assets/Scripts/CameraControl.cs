using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	public Vector2 min;
	public Vector2 max;
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
		Vector3 pos = transform.position;
		pos.x = Mathf.Clamp (pos.x, min.x, max.x);
		pos.y = Mathf.Clamp (pos.y, min.y, max.y);
		transform.position = pos;
	}
}
