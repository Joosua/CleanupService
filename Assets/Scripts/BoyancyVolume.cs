using UnityEngine;
using System.Collections;

public class BoyancyVolume : MonoBehaviour {
	public Transform waterlevel;
	void OnTriggerStay(Collider other) {

		if (other.attachedRigidbody && waterlevel){
			float depth =  waterlevel.position.y - other.attachedRigidbody.transform.position.y;
			other.attachedRigidbody.AddForce(Vector3.up * (6.0f + 15.0f*depth));
		}
	}
}
