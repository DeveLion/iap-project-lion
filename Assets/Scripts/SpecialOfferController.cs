using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CompleteProject
{
	public class SpecialOfferController : MonoBehaviour
	{

		public GameObject specialOfferPanel;
		public GameObject splashPanel;

		public USplashScreenUI uSplashScreenUI;



		// Use this for initialization
		void Start()
		{
			CheckForSpecialOffer();
		}

		// Update is called once per frame
		void Update()
		{
            if (Input.GetKeyDown(KeyCode.Space))
            {
				PlayerPrefs.DeleteAll();
            }
		}


		private void CheckForSpecialOffer()
		{
            if (PlayerPrefs.GetInt("SpecialOffer") == 1)
            {
				//no show offer
				specialOfferPanel.SetActive(false);
				splashPanel.SetActive(true);
				uSplashScreenUI.enabled = true;
			}
            else
            {
				//show offer
				specialOfferPanel.SetActive(true);
				splashPanel.SetActive(false);
				uSplashScreenUI.enabled = false;
			}

		}

		public void LeaveSpecialOffer()
        {
			PlayerPrefs.SetInt("SpecialOffer", 1);
			specialOfferPanel.SetActive(false);
			splashPanel.SetActive(true);
			uSplashScreenUI.enabled = true;
		}

		public void OfferBuyed()
        {
			PlayerPrefs.SetInt("SpecialOffer", 1);
			specialOfferPanel.SetActive(false);
			splashPanel.SetActive(true);
			uSplashScreenUI.enabled = true;
		}
	}

	
}

