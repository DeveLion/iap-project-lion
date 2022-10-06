using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

public class DragonController : MonoBehaviour {
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
	public bool IsFlying,IsRunning,Left,Right;
	[HideInInspector]
	public bool FireThrowAttack;
	[HideInInspector]
	public Vector3 MoveVector;
	[HideInInspector]
	public Camera Cam;
	[HideInInspector]
	public float multiplier;
	public Joystick MoveJS;
	bool isMoveable;
	public Slider Slide;
	public Slider EnemyHealth;
	[HideInInspector]
	public GameObject EngagedEnemy;
	public GameObject FireParticle,AttackCamPos,WalkPos;
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
		StartCoroutine (waitforStart());
		PlayerHealth.maxValue = TotalHealth;
		PlayerHealth.value = Health;
	}

	IEnumerator waitforStart(){
		yield return new WaitForSeconds (4f);
		isMoveable = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if (!isDead && isMoveable) {
			////////////////////////////flying controls////////////////////
			if (IsFlying) {
				//MoveVector.y = gameObject.transform.position.y + Slide.value;
				if (transform.position.y < Slide.value)
					transform.position = new Vector3 (transform.position.x, Slide.value-90, transform.position.z);
			} else {
					Slide.gameObject.SetActive (false);
			}
			///////////////////////////////// rotation control//////////////////

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
			///////////////////////////////// Animation control////////////////// 
			if (IsRunning) {
					multiplier = 2.5f;
				if (MoveJS.isMoving) {
					Anim.SetBool ("Run", true);
					Anim.SetBool ("Walk", false);
				} else {
					Anim.SetBool ("Run", false);
					Anim.SetBool ("Walk", false);
				}
			} else {
				multiplier = 1f;
				if (MoveJS.isMoving) {
					Anim.SetBool ("Walk", true);
					Anim.SetBool ("Run", false);
				} else {
					Anim.SetBool ("Run", false);
					Anim.SetBool ("Walk", false);
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
			print ("y position "+transform.position.y);
			transform.Translate (MoveVector * horizontalSpeed * multiplier);
			print ("y position "+transform.position.y);

		}

	}
	/// <summary>
	/// ///////////////////player health calculation /////////////////////////////////////////
	/// </summary>
	public void HealthDec(){
		Health--;
		if (Health > 0) {
			PlayerHealth.value = Health;
			Anim.SetTrigger ("GetHit");
		} else {
			PlayerHealth.value = Health;
			print ("health worked");
			isDead = true;
			Anim.SetTrigger("Death");
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

}
