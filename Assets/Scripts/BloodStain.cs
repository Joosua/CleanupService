﻿using UnityEngine;
using System.Collections;

public class BloodStain : MonoBehaviour {
	float amount;
	public float maxblood = 100f;
	//Material mat;
	public float startcutout;
	// Use this for initialization
	void Start () {
		amount = 0;
		//mat = new Material (renderer.material);
		startcutout = renderer.material.GetFloat ("_Cutoff");
		//renderer.material = mat;

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
		renderer.material.SetFloat ("_Cutoff", alpha);
		if (amount > maxblood - maxblood * 0.1f) {
			gameObject.renderer.enabled =false;
			Destroy(gameObject);
		}
	}

}
