using UnityEngine;
using System.Collections;

public class JointBreaker : MonoBehaviour {
	public Joint joint;
	public float breakLimit = 100;
	public float Damage;
	public ParticleSystem particleeffect_prefab;
	public GameObject[] enableOnBreak;
	// Use this for initialization
	void Start () {
		if (!joint) {
			joint = GetComponent<Joint>();
		}
		for (int i = 0; i < enableOnBreak.Length; i++) {
			enableOnBreak[i].renderer.enabled = false;
		}
	}


	void OnCollisionEnter(Collision collision) {
		foreach (ContactPoint contact in collision.contacts) {
			Debug.DrawRay(contact.point, contact.normal, Color.white);

		}
		if(collision.contacts[0].otherCollider.gameObject.layer == LayerMask.NameToLayer("Blade")){
			Damage += collision.relativeVelocity.magnitude * 9;
			SpawnEffect(collision.contacts[0].point);
		}
		else{
			Damage += collision.relativeVelocity.magnitude * 0.5f;
		}
		if(Damage > breakLimit){
			joint.connectedBody = null;
			OnBreak();
			Destroy(joint);
			Destroy(this);

		}
		/*
		if (collision.relativeVelocity.magnitude > 2)
			audio.Play();
		*/
	}

	void OnBreak(){
		for (int i = 0; i < enableOnBreak.Length; i++) {
			enableOnBreak[i].renderer.enabled = true;
		}
	}
	void SpawnEffect(Vector3 pos){
		if (particleeffect_prefab) {
			ParticleSystem ps = GameObject.Instantiate (particleeffect_prefab) as ParticleSystem;
			ps.transform.position = pos;
			Destroy (ps.gameObject, ps.duration-0.1f);
		}
	}
}
