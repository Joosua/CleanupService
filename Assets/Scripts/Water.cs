using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {
	public GameObject watervisual;
	public Vector2 endoffset;
	public Vector2 defaultoffset;
	public Vector2 targetoffset;
	public bool animateAcid;
	public bool isAcid;
	public ParticleSystem acidEffect;
	float acidState;
	// Use this for initialization
	void Start () {
		defaultoffset = watervisual.renderer.material.mainTextureOffset;
		isAcid = false;
		animateAcid = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (animateAcid) {
			acidState += Time.deltaTime * 0.25f;
			watervisual.renderer.material.mainTextureOffset = Vector2.Lerp(defaultoffset, targetoffset, acidState);
			if(acidState >= 1.0){
				animateAcid = false;
			}
		}
	}

	public void AnimateAcid(){
		if (isAcid == false) {
			isAcid = true;
			animateAcid = true;
			acidState = 0.0f;
			targetoffset = endoffset;
			acidEffect.enableEmission = true;
		}
	}

	void OnTriggerStay(Collider other) {
		
		if (other.attachedRigidbody){
			if(other.gameObject.GetComponent<Acid>()){
				AnimateAcid();
			}
		}
		if (isAcid) {
			AcidDisposable disp = other.gameObject.GetComponent<AcidDisposable>();
			if(disp){
				disp.UpdateDispose();
			}
		}
	}
}
