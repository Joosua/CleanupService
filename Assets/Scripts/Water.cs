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
	public int dissolveDir = 1;
	public float dissolved;
	public float dissolvedmax;
	public float effectStartEmit;
	float acidState;
	// Use this for initialization
	void Start () {
		dissolved = 0;
		defaultoffset = watervisual.renderer.material.mainTextureOffset;
		effectStartEmit = acidEffect.emissionRate;
		//isAcid = false;
		//animateAcid = false;
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
		if(acidEffect){
			if(dissolveDir > 0){
				acidEffect.emissionRate = effectStartEmit * dissolved / dissolvedmax;
			}
			else{
				acidEffect.emissionRate = effectStartEmit * (1.0f-(dissolved / dissolvedmax));
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
		if(dissolved < dissolvedmax){
			if (other.attachedRigidbody){
				if(other.gameObject.GetComponent<Acid>()){
					AnimateAcid();
				}
			}
			if (isAcid) {
				AcidDisposable disp = other.gameObject.GetComponent<AcidDisposable>();
				if(disp){
					dissolved += disp.UpdateDispose();
				}
			}
		}
	}
}
