using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsController : MonoBehaviour {
	GoogleMobileAdsHandler interstitial;
	public bool showInterstitalAtStart;
	public Text show;
	public string UnityAndroidID,UnityIosID;
	public static AdsController instance;

	public int canShowAds;


	void Start(){
		instance = this;
		interstitial = GetComponent<GoogleMobileAdsHandler> ();

		canShowAds = PlayerPrefs.GetInt("Ads");

        if (canShowAds == 0)
        {
			//show ads
			Debug.Log("can show ads");
        }
        else
        {
			//no show ads
			Debug.Log("cant show ads");
		}

		//interstitial.RequestInterstitial ();
		StartCoroutine(waitForRequest());
		if (!Advertisement.isInitialized && canShowAds == 1) {
			#if UNITY_ANDROID
			{
				//Initialize ad
				Advertisement.Initialize (UnityAndroidID, false);
			}
			#elif UNITY_IPHONE
		{
		//Initialize ad
		Advertisement.Initialize(UnityIosID,false);
		}
			#endif
		}
	}

	IEnumerator waitForRequest(){
		yield return new WaitForSeconds (1);
		interstitial.RequestInterstitial ();
	}

	void Update(){
		if(show!= null){
			show.text = interstitial.displayingText;
		}
	}

	public void OnInterstitialShow(){
		interstitial.ShowInterstitial ();
		interstitial.RequestInterstitial ();
		if(show!= null){
					show.text = "The ad was successfully shown.";
					}
	}

	public  void ShowInterstitialWithWait(float time){
		if(show!= null){
			show.text = "waiting"+time;
		}
		StartCoroutine (waitForShowI (time));
	}

	IEnumerator waitForShowI(float time){
		yield return new WaitForSeconds (time);
		OnInterstitialShow ();
	}

	/// <summary>
	/// Ad mob Controller
	/// Commet if not needed
	/// </summary>

	/// <summary>
	/// Unity Ads Controls
	/// Comment when not needed
	/// </summary>

	public void ShowUnityAd()
		{
			if (Advertisement.IsReady() && canShowAds == 0)
			{
				Advertisement.Show();
			}

		print ("Showing unRewarded");
		}

	public void ShowRewardedAd()
	{
		StartCoroutine(showAdsWithTimeOut(3));
		if (Advertisement.IsReady("rewardedVideoZone"))
		{
			//var options = new ShowOptions { resultCallback = HandleShowResult };
			//Advertisement.Show("rewardedVideoZone", options);

		}
		if(show!= null){
		show.text = "Showing Rewarded";
		}
		print ("Showing Rewarded");
	}
		
	private void HandleShowResult(ShowResult result)
	{
	switch (result)
	{
	case ShowResult.Finished:
		Debug.Log("The ad was successfully shown.");
		//
		if(show!= null){
		show.text = "The ad was successfully shown.";
		}
		// YOUR CODE TO REWARD THE GAMER
		// Give coins etc.
		break;
	case ShowResult.Skipped:
		Debug.Log("The ad was skipped before reaching the end.");
		if(show!= null){
		show.text = "The ad was skipped before reaching the end.";
		}
		break;
	case ShowResult.Failed:
		Debug.LogError("The ad failed to be shown.");
		break;
	}
	}
		
	IEnumerator showAdsWithTimeOut(float timeOut)
	{
		//Check if ad is supported on this platform 
		if (!Advertisement.isSupported)
		{
			Debug.LogError("<color=red>Ad is NOT supported</color>");
			if(show!= null){
				show.text = "Ad is NOT supported";
			}
			yield break; //Exit coroutine function because ad is not supported
		}
		bool adIsReady = false;
		Debug.Log("<color=green>Ad is supported</color>");
		if(show!= null){
			show.text = "Ad is supported";
		}
		//Initialize ad if it has not been initialized
		if (!Advertisement.isInitialized && canShowAds == 0) {
			#if UNITY_ANDROID
			{
				//Initialize ad
				Advertisement.Initialize (UnityAndroidID, false);
			}
			#elif UNITY_IPHONE
			{
			//Initialize ad
			Advertisement.Initialize(UnityIosID,false);
			}
			#endif
			print ("initializg again");
		} else {
			print ("already initialied");
			adIsReady = true;
		}


		float counter = 0;


		// Wait for timeOut seconds until ad is ready
		while(counter<timeOut && !adIsReady){
			counter += Time.deltaTime;
			if( Advertisement.IsReady ("rewardedVideoZone")){
				adIsReady = true;
				if(show!= null){
					show.text = "ready";
				}
				break; //Ad is //Ad is ready, Break while loop and continue program
			}
			yield return null;
		}

		//Check if ad is not ready after waiting
		if(!adIsReady){
			Debug.LogError("<color=red>Ad failed to be ready in " + timeOut + " seconds. Exited function</color>");
			yield break; //Exit coroutine function because ad is not ready
			if(show!= null){
				show.text = "Ad failed to be ready in \" + timeOut + \" seconds. Exited function";
			}
		}

		Debug.Log("<color=green>Ad is ready</color>");

		//Check if zoneID is empty or null
		if (string.IsNullOrEmpty("rewardedVideoZone"))
		{
			if(show!= null){
				show.text = "zoneId is null or empty. Exited function";
			}
			Debug.Log("<color=red>zoneId is null or empty. Exited function</color>");
			yield break; //Exit coroutine function because zoneId null
		}

		Debug.Log("<color=green>ZoneId is OK</color>");
		if(show!= null){
			show.text = "ZoneId is OK";
		}

		//Everything Looks fine. Finally show ad (Missing this part in your code)
		ShowOptions options = new ShowOptions();
		options.resultCallback = HandleShowResult;

		Advertisement.Show("rewardedVideoZone", options);
	}

}

