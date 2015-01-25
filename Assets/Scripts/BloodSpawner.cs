using UnityEngine;
using System.Collections;

public class BloodSpawner : MonoBehaviour {
	public BloodStain[] bloodStain_prefab;
	Vector3 lastSpawnPos;
	float thresshold = 0.25f;
	// Use this for initialization
	void Start () {
		
	}

	void OnCollisionStay(Collision collision) {
		if (collision.contacts [0].otherCollider.gameObject.layer == LayerMask.NameToLayer ("VisionBlocker") 
		    // || collision.contacts [0].otherCollider.gameObject.layer == LayerMask.NameToLayer ("Default")
		    ) {
			foreach (ContactPoint contact in collision.contacts) {
				Debug.DrawRay (contact.point, contact.normal, Color.white);

			}
			int rndindex = Random.Range (0, bloodStain_prefab.Length);
			if (rndindex > bloodStain_prefab.Length) {
				rndindex = bloodStain_prefab.Length;
			}
			Vector3 pos = collision.contacts [0].point;
			pos.z = bloodStain_prefab [rndindex].gameObject.transform.position.z;
			pos.y += 0.2f;
			float dst = Vector3.Distance (lastSpawnPos, pos);
			if (bloodStain_prefab [rndindex] && dst >= thresshold) {
				GameObject stain = GameObject.Instantiate (bloodStain_prefab [rndindex].gameObject) as GameObject;

				stain.transform.position = pos;
				float scl = 0.25f + Random.Range (0, 0.4f);
				stain.transform.localScale = new Vector3 (scl, scl, scl);
				stain.transform.eulerAngles = new Vector3 (0f, 0f, Random.value * 40 - 20);
				lastSpawnPos = stain.transform.position;
				thresshold = Random.Range(0.35f, 0.75f);
			}
						/*if(collision.contacts[0].otherCollider.gameObject.layer == LayerMask.NameToLayer("Blade")){
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
		
	}*/
					/*
	if (collision.relativeVelocity.magnitude > 2)
		audio.Play();
	*/
		}
	}
}
