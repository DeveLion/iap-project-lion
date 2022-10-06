using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UIController : MonoBehaviour {
	AnimalCharacterController DC;
	public Sprite Up,Down,Run,Walk;
	bool isFireAvailable;
	public GameObject FireNotAvailablePopup;
	void Start(){
		if (ManagingScript.TotalLevelCompleted > 5) {
			isFireAvailable = true;
		} else {
			isFireAvailable = false;
		}
		DC = FindObjectOfType<AnimalCharacterController> ();
	}

//	void Update(){
//		if (!DC.IsFlying) {
//		if (Input.GetKey (KeyCode.F)) {
//				DC.Anim.SetBool (DC.Attack, true);
//				ActionController.isAttacking = true;
//					DC.Cam.gameObject.transform.SetParent (DC.AttackCamPos.transform);
//					DC.Cam.gameObject.transform.localPosition = Vector3.zero;
//					DC.Cam.gameObject.transform.localRotation = Quaternion.identity;
//				} else {
//				DC.Anim.SetBool (DC.Attack, false);
//				ActionController.isAttacking = false;
//					DC.Cam.gameObject.transform.SetParent (DC.WalkPos.transform);
//					DC.Cam.gameObject.transform.localPosition = Vector3.zero;
//					DC.Cam.gameObject.transform.localRotation = Quaternion.identity;
//			}
//		}
//
//		if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.W)) {
//			if (DC.IsRunning) {
//				DC.Anim.SetBool (DC.Run, true);
//			} else {
//				DC.Anim.SetBool (DC.Walk, true);
//			}
//		}else{
//			DC.Anim.SetBool (DC.Run, false);
//			DC.Anim.SetBool (DC.Walk, false);
//		}
//	}

	public void OnMoveForwardPressed(bool Val){
		if (DC.IsFlying) {
			if (Val) {
				DC.ForwardSpeed = DC.horizontalSpeed;
			} else {
				DC.ForwardSpeed = 0;
			}
		} else if (DC.IsRunning) {
			DC.Anim.SetBool (DC.Run, Val);
			print ("working5");
		} else {
			DC.Anim.SetBool (DC.Walk, Val);
		}
	}

	public void OnJump(bool Val){
		if (!DC.IsFlying) {
			DC.Anim.SetBool (DC.Jump, Val);
			DC.IsRunning = Val;
		}
	}

	public void OnRunChange(Image MyImg){
		DC.IsRunning = !DC.IsRunning;
		if (DC.IsRunning) {
			MyImg.sprite = Walk;
		} else {
			MyImg.sprite = Run;
		}
	}

	public void OnFlyChange(Image MyImg){
		DC.IsFlying = !DC.IsFlying;
		if (DC.IsFlying) {
			//DC.Anim.SetTrigger ("StartFly");
			MyImg.sprite = Down;
			DC.GetComponent<Rigidbody> ().isKinematic = true;
		} else {
			MyImg.sprite = Up;
			//DC.Anim.SetTrigger ("Landing");
			DC.GetComponent<Rigidbody> ().isKinematic = false;
		}
	}

//	IEnumerator WaitForFlying(){
//		transform.Translate (Vector3.up);
//		yield return new WaitForSeconds (1);
//	}
	public void OnAttack(bool Val){
		if (!DC.IsFlying) {
			DC.Anim.SetBool (DC.Attack, Val);
			ActionController.isAttacking = Val;
			if (Val) {
				DC.Cam.gameObject.transform.SetParent (DC.AttackCamPos.transform);
				DC.Cam.gameObject.transform.localPosition = Vector3.zero;
				DC.Cam.gameObject.transform.localRotation = Quaternion.identity;
			} else {
				DC.Cam.gameObject.transform.SetParent (DC.WalkPos.transform);
				DC.Cam.gameObject.transform.localPosition = Vector3.zero;
				DC.Cam.gameObject.transform.localRotation = Quaternion.identity;
			}
		}
	}

	public void OnFlameAttack(bool Val){
		if (isFireAvailable) {
			DC.FireParticle.SetActive (Val);
			DC.Anim.SetBool (DC.Roar, Val);
			if (Val) {
				DC.FireParticle.GetComponent<ParticleSystem> ().Play ();
			} else {
				DC.FireParticle.GetComponent<ParticleSystem> ().Stop ();
			}

			if (Val) {
				DC.Cam.gameObject.transform.SetParent (DC.AttackCamPos.transform);
				DC.Cam.gameObject.transform.localPosition = Vector3.zero;
				DC.Cam.gameObject.transform.localRotation = Quaternion.identity;
			} else {
				DC.Cam.gameObject.transform.SetParent (DC.WalkPos.transform);
				DC.Cam.gameObject.transform.localPosition = Vector3.zero;
				DC.Cam.gameObject.transform.localRotation = Quaternion.identity;
			}
		} else {
			FireNotAvailablePopup.SetActive (Val);
		}

	}

	public void RotateLeft(bool Val){
		if (Val) {
			DC.Left = true;
//			if(!DC.MoveJS.isMoving)
//			DC.Anim.SetBool ("Left",true);
		} else {
			DC.Left = false;
//			if(!DC.MoveJS.isMoving)
//			DC.Anim.SetBool ("Left",false);
		}
	}

	public void RotateRight(bool Val){
		if (Val) {
			DC.Right = true;
//			if(!DC.MoveJS.isMoving)
//			DC.Anim.SetBool ("Right",true);
		} else {
			DC.Right = false;
//			if(!DC.MoveJS.isMoving)
//			DC.Anim.SetBool ("Right",false);
		}
	}
}
