using UnityEngine;
using System.Collections;

public class AnimalAIController : MonoBehaviour {
	public float Range;
	public string Attack,RunAttack,Look, Death,GetHit;
	GameObject Player;
	public float RunSpeed = 1,RotateSpeed = 1;
	Animation Anim;
	public int TotalHealth;
	public int Health = 5;
	bool isColliding,isDamaging;
	public bool isDead = false,isBoss = false;
	float distance;
	public float DistanceFromPlayer = 12f;
	// Use this for initialization
	void Start () {
		

		Anim = GetComponent<Animation> ();
		Anim.Play (Look);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!isDead) {
			//this.transform.rotation = Quaternion.Euler (0,-this.transform.rotation.y,0);
			Player = GameObject.FindGameObjectWithTag ("Player");
			if (Player != null) {
				distance = Vector3.Distance (transform.position, Player.transform.position);
				//this.transform.Rotate (Player.transform.position);
				transform.Rotate (0, Input.GetAxis ("Horizontal"), 0);
				if (Player.GetComponent<AnimalCharacterController> ().isGrounded && !Player.GetComponent<AnimalCharacterController> ().isDead) {
					if (distance < Range) {

						Vector3 Pos = Player.transform.position;
						Pos.y = this.transform.position.y;
						transform.LookAt (Pos);
						///////////////////////// ROTATE TOWARD PLAYER//////////////////					
						if (distance > DistanceFromPlayer) {
							isColliding = false;
								Anim.Play (RunAttack);
							print ("runattack"+distance);
							transform.position = Vector3.Lerp (transform.position, Pos, Time.deltaTime * RunSpeed);
//								Vector3 TargetPos = Player.transform.position;
//								TargetPos.y = transform.position.y;
//								Vector3 targetDir = Player.transform.position - transform.position;
//								float step = RotateSpeed * Time.deltaTime;
//								Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0F);
//								print ("working");
//								transform.rotation = Quaternion.LookRotation (newDir);
						} else {
							isColliding = true;
							if (!isDamaging) {
								isDamaging = true;
								Anim.Play (Attack);
								print ("playing attack");
								StartCoroutine (WaitForDamage ());
							}
						}
					} else {
						Anim.Play (Look);
						isDamaging = false;
						isColliding = false;
					}
				} else {
					
					Anim.Play (Look);
					isDamaging = false;
					isColliding = false;
				}
			}
		}

	}

	public void Hitted(){
		if (!isDead) {
			Anim.Stop ();
			Health--;
			if (Health > 0) {
				Anim.Play (GetHit);
			} else {
				Anim.Play (Death);
				isDead = true;
				if (isBoss) {
					LevelController.IsBossDone = true;
				}
				LevelController.IsNext = true;
				Player.GetComponent<AnimalCharacterController> ().HideEmenyHealthBar ();
			}
		}
	}

	IEnumerator WaitForDamage(){
		yield return new WaitForSeconds (3.5f);
		if (!isDead && !Player.GetComponent<AnimalCharacterController> ().IsFlying) {
			if (isDamaging) {
				Player.GetComponent<AnimalCharacterController> ().HealthDec ();
				StartCoroutine (WaitForDamage ());
			} else {
				isColliding = false;
			}
		} else {
			isDamaging = false;
		}
	}


}
