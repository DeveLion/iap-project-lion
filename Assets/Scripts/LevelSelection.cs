using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelection : MonoBehaviour {
	public GameObject UnlockPopUp,SelectVehiclePopup;
	public GameObject EndBlackScreen;
	public Scrollbar Sbar;
	float Value;
	float X = 0.0f;
	int Y = 0;
	void Start(){
		ManagingScript.LevelLoaded = 1;
		Value = 0.04f * (ManagingScript.TotalLevelCompleted-1);
		//StartCoroutine (WaitForScroll());
	}

	IEnumerator WaitForScroll(){
		yield return new WaitForSeconds (0.02f);
		X += 0.05f;
		Sbar.value = X;
		if (X < Value) {
			StartCoroutine (WaitForScroll());
		}else{
			Sbar.value = Value;
		}
	}

	public void Button(levelID level){
		print ("level lock " + level.isLocked);
		if (level.isLocked == 1) {
				ManagingScript.LevelLoaded = level.levelId;
				StartCoroutine (endScreenCoroutine (4));
				level.gameObject.GetComponent<Animator> ().SetTrigger ("Play");
		//Application.LoadLevel (4);
		} else {
			UnlockPopUp.SetActive (true);
		}
	}

	public void levelLoad(int levelnum){
		StartCoroutine(endScreenCoroutine(levelnum));
	}

	IEnumerator endScreenCoroutine(int num){
		EndBlackScreen.SetActive (true);
		yield return new WaitForSeconds (1.2f);
		Application.LoadLevel (num);
		if (num == 4) {
			ManagingScript.isDecreaseVolume = true;
		}
	}



}
