using UnityEngine;
using System.Collections;
using ChartboostSDK;

public class ManagingScript : MonoBehaviour {
	public static int VehicleNo;
	public static int Amount;
	public static int LevelLoaded;
	public static int TotalLevelCompleted;
	public static bool lastLevelCompleted;
	public static bool levelCleared;
	public static int control,sound,quality;
	public GameObject BGsound;
	public static bool isDecreaseVolume,isIncreaseVolume;
	float currentVolume;
	// Use this for initialization
	void Awake() {
		levelCleared = false;
		if(LevelLoaded == 0)
		LevelLoaded = 1;
		DontDestroyOnLoad (this.gameObject);
		//PlayerPrefs.SetInt ("amount", 1000);
		VehicleNo = PlayerPrefs.GetInt ("vehicleno",0);
		Amount = PlayerPrefs.GetInt ("amount", 500);
		//PlayerPrefs.SetInt ("totalLevelcompleted",1);
		TotalLevelCompleted = PlayerPrefs.GetInt ("unlock",1);
		print ("total level complete MS"+TotalLevelCompleted);
		control = PlayerPrefs.GetInt ("control",2);
		sound = PlayerPrefs.GetInt ("sound",0);
	//	graphics = PlayerPrefs.GetInt ("graphics",0);
		quality = PlayerPrefs.GetInt ("quality",0);
		Chartboost.cacheInterstitial(CBLocation.Default);
		StartCoroutine (WaitForAdCheck());
	}


	IEnumerator WaitForAdCheck(){
		yield return new WaitForSeconds (4);
		if (!Chartboost.hasInterstitial (CBLocation.Default)) {
			Chartboost.cacheInterstitial (CBLocation.Default);
		} else {
			StartCoroutine (WaitForAdCheck ());
		}
	}
	
	void Update(){
		if (BGsound != null) {
			if (sound == 1 && Application.loadedLevel != 0) {
				BGsound.SetActive (false);
			} else {
				BGsound.SetActive (true);
			}
		}

//		if(isDecreaseVolume){
//			isDecreaseVolume = false;
//			StartCoroutine (DecreaseVolume());
//		}
//
//		if(isIncreaseVolume){
//			isIncreaseVolume = false;
//			StartCoroutine (IncreaseVolume());
//		}
	}

	IEnumerator DecreaseVolume(){
		currentVolume = BGsound.GetComponent<AudioSource> ().volume;
		yield return new WaitForSeconds (0.1f);
		BGsound.GetComponent<AudioSource> ().volume -= 0.1f;
		if (BGsound.GetComponent<AudioSource> ().volume > 0) {
			StartCoroutine (DecreaseVolume());
		} else{
				BGsound.GetComponent<AudioSource> ().volume = 0;
			}
	}

	IEnumerator IncreaseVolume(){
		yield return new WaitForSeconds (0.1f);
		BGsound.GetComponent<AudioSource> ().volume += 0.1f;
		if (BGsound.GetComponent<AudioSource> ().volume < currentVolume) {
			StartCoroutine (DecreaseVolume ());
		} else {
			BGsound.GetComponent<AudioSource> ().volume = currentVolume;
		}

	}

	public static void SetVehicleNo(int Vehicle){
		VehicleNo = Vehicle;
		PlayerPrefs.SetInt ("vehicleno",VehicleNo);
	}

	public static void SetAmount(int amount){
		Amount = amount;
		PlayerPrefs.SetInt ("amount",Amount);
		print (Amount);
	}

	public static void onLevelCleared(){
		if(TotalLevelCompleted == LevelLoaded){
		TotalLevelCompleted++;
		}
		PlayerPrefs.SetInt ("unlock",TotalLevelCompleted);
		print ("Level complete"+TotalLevelCompleted);
	}
		
	public static void setSound(){
		PlayerPrefs.SetInt ("sound",sound);
	}

	public static void setControl(){
		PlayerPrefs.SetInt ("control",control);
	}

	public static void setQuality(){
		PlayerPrefs.SetInt ("quality",quality);
	}

//	public static void setGraphics(){
//		PlayerPrefs.SetInt ("graphics",graphics);
//	}
}
