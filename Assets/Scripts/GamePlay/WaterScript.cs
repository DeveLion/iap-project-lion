using UnityEngine;
using System.Collections;

public class WaterScript : MonoBehaviour {
	AnimalCharacterController Player;
	bool isColliding = false;
	// Use this for initialization
	void Start () {
		
	}

	void Update(){
		if(GameObject.FindGameObjectWithTag ("Player").GetComponent<AnimalCharacterController> () != null)
			Player = GameObject.FindGameObjectWithTag ("Player").GetComponent<AnimalCharacterController> ();
	}

	void OnCollisionEnter(Collision col){
		if(!Player.isDead)
		if (col.collider.tag == "Player") {
			isColliding = true;
			StartCoroutine (WaitForDamage());
		}
	}

	IEnumerator WaitForDamage(){
		yield return new WaitForSeconds (3.5f);
		if (!Player.isDead) {
			if (isColliding) {
				Player.HealthDec ();
				StartCoroutine (WaitForDamage ());
			}
		}
	}

	void OnCollisionExit(Collision col){
		if(!Player.isDead)
		if (col.collider.tag == "Player") {
			isColliding = false;
		}

	}
}
