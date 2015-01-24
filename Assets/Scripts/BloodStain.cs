using UnityEngine;
using System.Collections;

public class BloodStain : MonoBehaviour {
	float amount;
	public float maxblood = 100f;
	Material material;
	public float startcutout;
	// Use this for initialization
	void Start () {
		amount = 0;
		material = new Material (renderer.material);
		renderer.material = material;
		startcutout = material.GetFloat ("_Cutoff");
	}
	
	void OnTriggerEnter(Collider other) {
		Rag rag = other.GetComponent<Rag> ();
		if (rag) {
			RemoveBlood (rag.cleanupPower);
		}
	}
	void OnTriggerStay(Collider other) {
		//print ("SDFDFASDFSD" + amount);
		Rag rag = other.GetComponent<Rag> ();
		if (rag) {
			RemoveBlood (rag.cleanupPower * Time.deltaTime);
		}
	}

	void RemoveBlood(float removeamount){
		amount += removeamount;
		float alpha = startcutout + (1.0f - startcutout) * (amount/maxblood);
		print (alpha);
		material.SetFloat ("_Cutoff", alpha);

		if (amount > maxblood) {
			Destroy(gameObject);
		}
	}

}
