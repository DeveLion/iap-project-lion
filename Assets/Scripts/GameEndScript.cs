using UnityEngine;
using System.Collections;

public class GameEndScript : MonoBehaviour {
	public GameObject Cleared, Failed,EndBlackScreen;
	public static bool isShow;
	// Use this for initialization
	void Start () {
		
		if (ManagingScript.levelCleared) {
			Cleared.SetActive (true);
			Failed.SetActive (false);
		} else {
			Cleared.SetActive (false);
			Failed.SetActive (true);
		}
	}

	public void LoadLevelNum(int level){
		StartCoroutine(endScreenCoroutine(level));
	}

	public void NextLevel(){
		ManagingScript.LevelLoaded++;
		StartCoroutine(endScreenCoroutine(4));
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
