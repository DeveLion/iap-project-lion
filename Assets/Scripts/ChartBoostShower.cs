using UnityEngine;
using System.Collections;
using ChartboostSDK;
using System.Collections.Generic;
public class ChartBoostShower : MonoBehaviour {

	// Use this for initialization
	void Start () {
		print (Chartboost.hasInterstitial (CBLocation.Default));
		if (PlayerPrefs.GetInt ("Ads", 0) == 0) {
			if (Chartboost.hasInterstitial (CBLocation.Default)) {
				Chartboost.showInterstitial (CBLocation.Default);
				print ("shown");
			} else {
				print ("not showing");
				Chartboost.cacheInterstitial (CBLocation.Default);
			}
		}
	}
}
