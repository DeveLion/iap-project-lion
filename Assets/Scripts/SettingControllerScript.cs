using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingControllerScript : MonoBehaviour {
	//public GameObject steering,tilt,buttons;
//	public GameObject GHigh,Gmedium,Glow,Gverylow;
	//public GameObject Qhigh,Qmedium,Qlow,Qverylow;
	public Sprite soundon,soundoff;
	public Image SoundButton;
	//public bool isMainMenu;
	//public GameObject introDialog;
	public AudioListener AL;
	public GameObject EndBlackScreen;
	bool isSound;
	bool isShow;

	int Rated;
	// Use this for initialization
	void Start () {
		Rated = PlayerPrefs.GetInt ("Rate",0); 
		onSoundChange ();
//		ControlChange ();
//		if (Application.loadedLevel != 2) {
//			//graphicsChange ();
//			qualityChange ();
//		}
//		if (isMainMenu) {
//			if (PlayerPrefs.GetInt ("Intro",0)== 0) {
//				introDialog.SetActive (true);
//				PlayerPrefs.SetInt ("Intro",1);
//			}
//		}
	}

	public void closeApp(GameObject LinkDialog){
		if (Rated == 0 && isShow) {
			LinkDialog.SetActive (true);
			isShow = true;
		} else {
			Application.Quit ();
		}
	}

	public void closeAp(){
		Application.Quit ();
	}

	public void LoadLevelNum(int level){
		StartCoroutine (endScreenCoroutine(level));
	}
		
	public void MoreGames(){
		//Application.OpenURL ("https://play.google.com/store/apps/developer?id=Fun%20Simulator%20Studio%20-%20action%2C%20sim%20and%20racing%20game&hl=en");
		//Application.OpenURL ("https://itunes.apple.com/app/id1200632704");
	}

	public void RateUs(){
		PlayerPrefs.SetInt ("Rate",1); 
		//Application.OpenURL ("https://play.google.com/store/apps/details?id=com.fsstudio.com.fsstudio.flyingloinsimulator");
		Application.OpenURL ("https://itunes.apple.com/app/id1224840246");
	}

	public void Game1(){
		//Application.OpenURL ("https://play.google.com/store/apps/details?id=com.fsstudio.taxidrivingsim2017&hl=en");
		Application.OpenURL ("https://itunes.apple.com/app/id1198353953");
	}

	public void Game2(){
		//Application.OpenURL ("https://play.google.com/store/apps/details?id=com.fsstudio.speedparkingvaletmania");
		Application.OpenURL ("https://itunes.apple.com/app/id1198014029");
	}

	public void Game3(){
		//Application.OpenURL ("https://play.google.com/store/apps/details?id=com.fsstudio.offroadbikestunt&hl=en");
		Application.OpenURL ("https://itunes.apple.com/app/id1200055771");
	}

//	public void OnControlClick(int num){
//		ManagingScript.control = num;
//		ControlChange ();
//	}

// void ControlChange(){
//		if (ManagingScript.control == 0) { 
//			///// steering control
//			steering.SetActive (true);
//			tilt.SetActive (false);
//			buttons.SetActive (false);
//			ManagingScript.setControl ();
//		} else if (ManagingScript.control == 1) {
//			///// tilt control
//			steering.SetActive (false);
//			tilt.SetActive (true);
//			buttons.SetActive (false);
//			ManagingScript.setControl ();
//		} else if (ManagingScript.control == 2) {
//			//// button control
//			steering.SetActive (false);
//			tilt.SetActive (false);
//			buttons.SetActive (true);
//			ManagingScript.setControl ();
//		}
//	}

/*	public void OnGraphicsClick(int num){
		ManagingScript.graphics = num;
		graphicsChange ();
	}

	void graphicsChange(){
		if (ManagingScript.graphics == 0) {
			QualitySettings.shadows = ShadowQuality.All;
			QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
			GHigh.SetActive (true);
			Gmedium.SetActive (false);
			Glow.SetActive (false);
			Gverylow.SetActive (false);
			ManagingScript.setGraphics ();
		} else if (ManagingScript.graphics == 1) {
			QualitySettings.shadows = ShadowQuality.HardOnly;
			QualitySettings.shadowResolution = ShadowResolution.High;
			GHigh.SetActive (false);
			Gmedium.SetActive (true);
			Glow.SetActive (false);
			Gverylow.SetActive (false);
			ManagingScript.setGraphics ();
		} else if (ManagingScript.graphics == 2) {
			QualitySettings.shadows = ShadowQuality.HardOnly;
			QualitySettings.shadowResolution = ShadowResolution.Medium;
			GHigh.SetActive (false);
			Gmedium.SetActive (false);
			Glow.SetActive (true);
			Gverylow.SetActive (false);
			ManagingScript.setGraphics ();
		}else if (ManagingScript.graphics == 3) {
			QualitySettings.shadows = ShadowQuality.Disable;
			QualitySettings.shadowResolution = ShadowResolution.Low;
			GHigh.SetActive (false);
			Gmedium.SetActive (false);
			Glow.SetActive (false);
			Gverylow.SetActive (true);
			ManagingScript.setGraphics ();
		}
	}
*/
//	public void OnQulaityClick(int num){
//		ManagingScript.quality = num;
//		qualityChange ();
//	}
//
//	void qualityChange(){
//		if (ManagingScript.quality == 0) {
//			QualitySettings.SetQualityLevel (5);
//
//			Qhigh.SetActive (true);
//			Qmedium.SetActive (false);
//			Qlow.SetActive (false);
//			Qverylow.SetActive (false);
//			ManagingScript.setQuality ();
//		} else if (ManagingScript.quality == 1) {
//			QualitySettings.SetQualityLevel (3);
//			Qhigh.SetActive (false);
//			Qmedium.SetActive (true);
//			Qlow.SetActive (false);
//			Qverylow.SetActive (false);
//			ManagingScript.setQuality ();
//		} else if (ManagingScript.quality == 2) {
//			QualitySettings.SetQualityLevel (2);
//			Qhigh.SetActive (false);
//			Qmedium.SetActive (false);
//			Qlow.SetActive (true);
//			Qverylow.SetActive (false);
//			ManagingScript.setQuality ();
//		}else if (ManagingScript.quality == 3) {
//			QualitySettings.SetQualityLevel (0);
//			Qhigh.SetActive (false);
//			Qmedium.SetActive (false);
//			Qlow.SetActive (false);
//			Qverylow.SetActive (true);
//			ManagingScript.setQuality ();
//		}
//	}

	public void SoundClick(){
		isSound = !isSound;
		if(isSound)
			ManagingScript.sound = 0;
		else
			ManagingScript.sound = 1;
		onSoundChange ();
	}

	void onSoundChange(){
		if (ManagingScript.sound == 0) {
			AL.enabled = true;
			ManagingScript.setSound ();
			isSound = true;
			SoundButton.sprite = soundon;
			//soundoff.SetActive (false);
		}else{
			AL.enabled = false;
			isSound = false;
			ManagingScript.setSound ();
			SoundButton.sprite = soundoff;
			//soundoff.SetActive (true);
		}
	}

	public void CloseClicked(GameObject settingpanel){
		settingpanel.SetActive (false);
	}

	IEnumerator endScreenCoroutine(int num){
		EndBlackScreen.SetActive (true);
		yield return new WaitForSeconds (1.2f);
		Application.LoadLevel (num);
	}
	public void pause(){
		Time.timeScale = 0;

	}

	public void Play(){
		Time.timeScale = 1;
	}

	public void PlayLoadLevel(int num){
		Time.timeScale = 1;
		//Cam.GetComponent<RCC_Camera> ().enabled = true;
		StartCoroutine(endScreenCoroutine(num));
	}

	public void OnPlay(GameObject Dialog){
		int SettingCheck = PlayerPrefs.GetInt ("FirstSetting",0);
		if (SettingCheck == 1) {
			StartCoroutine (endScreenCoroutine (2));
		} else {
			Dialog.SetActive (true);
			PlayerPrefs.SetInt ("FirstSetting",1);
		}
	}
}
