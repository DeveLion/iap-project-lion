using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

public class AnimalCharacterController : MonoBehaviour {
	[HideInInspector]
	public Animator Anim;
	[HideInInspector]
	public Rigidbody rb;
	public float horizontalSpeed, VerticalSpeed, Amplitude,RotateSpeed;
	[HideInInspector]
	public float ForwardSpeed = 0;
	[HideInInspector]
	public Vector3 tempPos;
	[HideInInspector]
	public bool IsFlying,IsRunning,Left,Right,isGrounded;
	[HideInInspector]
	public bool FireThrowAttack;
	[HideInInspector]
	public Vector3 MoveVector;
	[HideInInspector]
	public Camera Cam;
	[HideInInspector]
	public float multiplier;
	public string Walk,Run,GetHit,Death,Bite,Jump,Roar,JumpAttack,Attack;
	public Joystick MoveJS;
	public Slider Slide;
	public Slider EnemyHealth;
	[HideInInspector]
	public GameObject EngagedEnemy;
	public GameObject Wings,FireParticle,AttackCamPos,WalkPos;
	public Slider PlayerHealth;
	LevelController LC;
	//[HideInInspector]
	public int TotalHealth = 10,Health = 10;
	public bool isDead;
	// Use this for initialization
	void Start () {
		LC = FindObjectOfType<LevelController> ();
		Cam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
		tempPos = transform.position;
		Anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody> ();
		PlayerHealth.maxValue = TotalHealth;
		PlayerHealth.value = Health;
		Wings.GetComponent<Animator> ().SetBool("fly",false);
	}

	// Update is called once per frame
	void FixedUpdate () {

		///////////////////////////////// rotation control//////////////////
		if (!isDead) {
		if (MoveJS.Horizontal () < 0) {
			Left = true;
			Right = false;
			RotateSpeed = -MoveJS.Horizontal ();
		} else if (MoveJS.Horizontal () > 0) {
			Left = false;
			Right = true;
			RotateSpeed = MoveJS.Horizontal ();
		} else {
			Left = false;
			Right = false;
		}

		if (Left) {
			transform.Rotate (new Vector3 (0, -RotateSpeed, 0));
			multiplier = 0.7f;
		} else if (Right) {
			multiplier = 0.7f;
			transform.Rotate (new Vector3 (0, RotateSpeed, 0));
		}


			////////////////////////////flying controls////////////////////
			if (IsFlying) {
				Anim.SetBool (Run,false);
				Anim.SetBool (Walk,false);
				//MoveVector.y = gameObject.transform.position.y + Slide.value;
				Wings.GetComponent<Animator> ().SetBool("fly",true);
				Vector3 FlyPos = new Vector3 (transform.position.x, Slide.value - 90, transform.position.z);
				//float distance = Vector3.Distance (transform.position, FlyPos);
				if (transform.position.y < Slide.value-90) {
					//transform.position = new Vector3 (transform.position.x, Slide.value - 90, transform.position.z);

					/////////////////try
					this.transform.Translate (Vector3.up * Time.deltaTime * VerticalSpeed);
				} else {
					this.transform.Translate (Vector3.down * Time.deltaTime * VerticalSpeed);
				}

			} else {
				Wings.GetComponent<Animator> ().SetBool("fly",false);
				Slide.gameObject.SetActive (false);
				///////////////////////////////// Animation control////////////////// 
				if (IsRunning) {
					multiplier = 2.5f;
					if (MoveJS.isMoving) {
						Anim.SetBool (Run, true);
						Anim.SetBool (Walk, false);
					} else {
						Anim.SetBool (Run, false);
						Anim.SetBool (Walk, false);
						//print ("working2");
					}
				} else {
					multiplier = 1f;
					if (MoveJS.isMoving) {
						Anim.SetBool (Walk, true);
						Anim.SetBool (Run, false);
					} else {
						Anim.SetBool (Run, false);
						Anim.SetBool (Walk, false);
						//	print ("working4");
					}
				}
			}
			///////////////////////////////// motion control//////////////////
			Vector3 dir = Vector3.zero;
			if (IsFlying) {
				dir.z = MoveJS.Vertical ();
				//dir.y = MoveJS.Horizontal ();
			} else {
				dir.z = MoveJS.Vertical ();
				//print (MoveJS.Horizontal ());
			}

			if (dir.magnitude > 1.0f)
				dir.Normalize ();
			MoveVector = dir;
			if (IsFlying) {
				Slide.gameObject.SetActive (true);
				//MoveVector.y = Mathf.Sin (Time.deltaTime * VerticalSpeed) * Amplitude;
				StartCoroutine (WaitForFly (0.2f));
			} else {
				Amplitude = 2;
			}
			if (IsFlying) {
				transform.Translate (MoveVector * horizontalSpeed * multiplier*2);
			} else {
				transform.Translate (MoveVector * horizontalSpeed * multiplier);
			}

		}

	}
	/// <summary>
	/// ///////////////////player health calculation /////////////////////////////////////////
	/// </summary>
	public void HealthDec(){
		Health--;
		if (Health > 0) {
			PlayerHealth.value = Health;
			Anim.SetTrigger (GetHit);
		} else {
			PlayerHealth.value = Health;
			print ("health worked");
			isDead = true;
			Anim.SetTrigger(Death);
			ManagingScript.levelCleared = false;
		}
	}


	IEnumerator WaitForFly(float no){
		yield return new WaitForSeconds (0.5f);
		Amplitude = no;
	}
	/// <summary>
	/// //////////enmy health calculations //////////////////////////////////
	/// </summary>
	/// <param name="emeny">Emeny.</param>
	public void OnEnemyEngage(GameObject emeny){
		if (EngagedEnemy != emeny) {
			EngagedEnemy = emeny;
			EnemyHealth.maxValue = emeny.GetComponent<AnimalAIController> ().TotalHealth;
			EnemyHealth.value = emeny.GetComponent<AnimalAIController> ().TotalHealth;
		}
		StartCoroutine (WaitForDamage ());
	}

	IEnumerator WaitForDamage(){
		LC.AnimalName (EngagedEnemy.name);
		EnemyHealth.gameObject.SetActive (true);
		yield return new WaitForSeconds (0.5f);
		EnemyHealth.value = EngagedEnemy.GetComponent<AnimalAIController> ().Health;
	}

	public void HideEmenyHealthBar(){
		StartCoroutine (WaitForHide (2));
	}

	IEnumerator WaitForHide(int no){
		yield return new WaitForSeconds (no);
		EnemyHealth.gameObject.SetActive (false);
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag== "Terrain") {
			isGrounded = true;
			print ("terrain "+isGrounded);
		}
	}

	void OnCollisionExit(Collision other){
		if (other.gameObject.tag== "Terrain") {
			isGrounded = false;
			print ("terrain "+isGrounded);
		}
	}

}
