using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeController : MonoBehaviour {
	public Text EaringText;
	//public int time,earning;
	public static bool isPicked,IsBoss;
	public static int totalEarning;
	public GameObject endblackscreen;
	// Use this for initialization
	void Start () {
		//StartCoroutine (waitforSec());
		totalEarning = 0;
		EaringText.text = ""+ totalEarning;  
		//time = ManagingScript.LevelLoaded*240;
	}
	
	// Update is called once per frame
	void Update () {
		if (isPicked) {
			totalEarning += 1;
			isPicked = false;
			EaringText.text = ""+ totalEarning;  
		}

		if (IsBoss) {
			totalEarning += 5;
			IsBoss = false;
			EaringText.text = ""+ totalEarning;  
		}
	}
//		
//	IEnumerator waitforSec(){
//		yield return new WaitForSeconds (1);
//		time -=1;
//		int x = time / 60;
//		int y = time % 60;
//		timeRemaining.text = x+":"+y+" Sec ";
//		if (time > 0) {
//			StartCoroutine (waitforSec ());
//		} else {
//			ManagingScript.levelCleared = false;
//			StartCoroutine (BlackScreenCouroutine(5));
//		}
//	}

	IEnumerator BlackScreenCouroutine(int level){
		endblackscreen.SetActive (true);
		yield return new WaitForSeconds (1f);
		Application.LoadLevel (level);

	}


}
