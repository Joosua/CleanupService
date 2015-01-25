using UnityEngine;
using System.Collections;

public class AcidDisposable : MonoBehaviour {
	public float resistance = 1.0f;
	float state = 0.0f;

	public void UpdateDispose(){
		state += Time.deltaTime* 0.2f * resistance;

		renderer.material.SetFloat ("_Cutoff", state);
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
			}
		}
	}
}
