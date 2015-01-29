using UnityEngine;
using System.Collections;

public class AcidDisposable : MonoBehaviour {
	public float resistance = 1.0f;
	float state = 0.0f;
	public Renderer[] subrenderers;
	public float UpdateDispose(){
		float amount = Time.deltaTime* 0.2f * resistance;
		state += amount;

		renderer.material.SetFloat ("_BurnMask", state);
		foreach(Renderer r in subrenderers){
			r.material.SetFloat ("_BurnMask", state);
		}
		if (state > 2.1f) {
			//Destroy(gameObject);
			collider.enabled = false;
			if(rigidbody){
				Joint j = rigidbody.GetComponent<Joint>();
				if(j){
					j.connectedBody = null;
					Destroy(j);
				}
				Destroy(rigidbody);
				Destroy(gameObject);
			}
		}
		return amount;
	}
}
