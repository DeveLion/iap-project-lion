using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VehicleDetails : MonoBehaviour {
	public int id;
	public GameObject lockImage;
	public int isUnlocked;
	public Text Price;
	public int price;
	// Use this for initialization
	void Awake () {
		if (id == 0) {
			lockImage.SetActive (false);
			Price.text = "FREE";
			isUnlocked = 1;
			//PlayerPrefs.SetInt ("unlock" + id, isUnlocked);
		} else {
			//isUnlocked = PlayerPrefs.GetInt ("unlock",0);
			if (PlayerPrefs.GetInt ("unlock", 5) == 5) {
				isUnlocked = 0;
			} else {
				isUnlocked = 1;
			}
			print ("isunlock "+PlayerPrefs.GetInt ("unlock", 5) +" "+ isUnlocked);
			if (isUnlocked == 1) {
				lockImage.SetActive (false);
			//	Price.text = "UnLocked";
			}
//			} else {
//				lockImage.SetActive (true);
//				Price.text = price+" $";
//			}
		}
	}

	public void Unlock(){
		isUnlocked = 1;
	//	PlayerPrefs.SetInt ("unlock" + id,isUnlocked);
		lockImage.SetActive (false);
		Price.text = "UnLocked";
	}
}
