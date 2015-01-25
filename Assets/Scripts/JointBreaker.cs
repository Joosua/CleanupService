using UnityEngine;
using System.Collections;

public class JointBreaker : MonoBehaviour {
	public Joint joint;
	public float breakLimit = 100;
	public float Damage;
	public ParticleSystem particleeffect_prefab;
	public BloodStain[] bloodStain_prefab;
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
		if (collision.contacts [0].otherCollider.gameObject.layer == LayerMask.NameToLayer ("Blade")) {
			Damage += collision.relativeVelocity.magnitude * 9;
			SpawnEffect(collision.contacts[0].point);

			int rndindex = Random.Range (0, bloodStain_prefab.Length);
			if (rndindex > bloodStain_prefab.Length) {
					rndindex = bloodStain_prefab.Length;
			}
			if (bloodStain_prefab.Length > 0 && bloodStain_prefab [rndindex]) {
				if (collision.relativeVelocity.magnitude > 0.5f) {
					Vector3 pos = collision.contacts [0].point;
					pos.z = bloodStain_prefab [rndindex].gameObject.transform.position.z;
					pos.y += 0.2f;

					GameObject stain = GameObject.Instantiate (bloodStain_prefab [rndindex].gameObject) as GameObject;

					stain.transform.position = pos;
					float scl = 0.25f + Random.Range (0, 0.4f);
					stain.transform.localScale = new Vector3 (scl, scl, scl);
					stain.transform.eulerAngles = new Vector3 (0f, 0f, Random.value * 40 - 20);
				}
			}


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
