using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CompleteProject
{

	public class ShopManager : MonoBehaviour
	{

        void onEnable()
        {
			if (PlayerPrefs.GetInt("SpecialOfferBuyed") == 1)
			{
				//no show shop
				this.gameObject.GetComponent<Button>().interactable = false;
			}
			else
			{
				//show shop
				this.gameObject.GetComponent<Button>().interactable = true;
			}
		}
		// Use this for initialization
		void Start()
		{
			
		}

		
	}
}
