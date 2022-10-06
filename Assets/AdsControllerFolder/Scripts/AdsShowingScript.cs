using UnityEngine;
using System.Collections;

public class AdsShowingScript : MonoBehaviour {
	public bool isGameOver;
	public static bool isAdsDone;
	public GameObject blackScreen;
	public static bool isPlayVideoAd;
	public static bool isStart = true;
	// Use this for initialization
	void Start () {
		StartCoroutine (StartScreenCoroutine());
		if (isStart) {
			isStart = false;
		} else {
			if (!isPlayVideoAd) {
				if (isGameOver) {
					AdsController.instance.ShowUnityAd ();
				} else {
					AdsController.instance.OnInterstitialShow ();
				}
			} else {
				isPlayVideoAd = false;
				AdsController.instance.OnInterstitialShow ();
			}
		}
	}
	
	public void ShowRewarded(){
		AdsController.instance.ShowRewardedAd ();
	}

	IEnumerator StartScreenCoroutine(){
		blackScreen.SetActive (true);
		yield return new WaitForSeconds (1.5f);
		blackScreen.SetActive (false);
	}

}
