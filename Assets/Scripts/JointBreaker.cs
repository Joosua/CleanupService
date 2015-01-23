using UnityEngine;
using System.Collections;

public class JointBreaker : MonoBehaviour {
	public Joint joint;
	public float breakLimit = 100;
	public float Damage;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		foreach (ContactPoint contact in collision.contacts) {
			Debug.DrawRay(contact.point, contact.normal, Color.white);

		}
		if(collision.contacts[0].otherCollider.gameObject.layer == LayerMask.NameToLayer("Knife")){
			Damage += collision.relativeVelocity.magnitude * 9;
			if(Damage > breakLimit){
				joint.connectedBody = null;
				Destroy(joint);
				Destroy(this);
			}
		}
		else{
			Damage += collision.relativeVelocity.magnitude * 0.5f;
			if(Damage > breakLimit){
				joint.connectedBody = null;
				Destroy(joint);
				Destroy(this);
			}
		}
		/*
		if (collision.relativeVelocity.magnitude > 2)
			audio.Play();
		*/
	}

}
