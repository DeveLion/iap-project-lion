using UnityEngine;
using System.Collections;

public class ActionController : MonoBehaviour {
	public static bool isAttacking;
	AnimalCharacterController DC;
	bool isparticleCollided;

	void Start(){
		DC = FindObjectOfType<AnimalCharacterController> ();
	}
	
	void OnTriggerEnter(Collider col){
		if (isAttacking) {
			if (col.tag == "Enemy") {
				col.GetComponent<AnimalAIController> ().Hitted ();
				DC.OnEnemyEngage (col.gameObject);
			}
		}
	}

	void OnParticleCollision(GameObject col){
		print ("working " + col.tag);
		if (col.tag == "Enemy" && !isparticleCollided) {
			col.GetComponent<AnimalAIController> ().Hitted ();
			DC.OnEnemyEngage (col.gameObject);
			StartCoroutine (WaitForParticleCollide());
		}
	}

	IEnumerator WaitForParticleCollide(){
		isparticleCollided = true;
		yield return new WaitForSeconds (0.5f);
		isparticleCollided = false;
	}
}
