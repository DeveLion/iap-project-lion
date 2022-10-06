using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VehicleSelector : MonoBehaviour {
	public GameObject[] vehicles;
	public GameObject[] vehiclesDetails;
	public GameObject BuyVehiclePopup,NotEnoughMoneyPop;
	public Text Amount;
	public GameObject EndBlackScreen;
	int selectedvehicleNo;
	public static bool isVehicleChanged= false;
	bool DisableChanging = false;
	public Text NameText;
	// Use this for initialization
	void Start () {
		if (ManagingScript.quality > 1) {
				QualitySettings.antiAliasing = 3;
				QualitySettings.SetQualityLevel (6);
		} else {
			QualitySettings.antiAliasing = 4;
			QualitySettings.SetQualityLevel (6);
		}
		Amount.text = ManagingScript.Amount+"";
		selectedvehicleNo =  ManagingScript.VehicleNo;
		vehicles [selectedvehicleNo].SetActive (true);
		NameText.text = vehicles [selectedvehicleNo].name;
		vehiclesDetails [selectedvehicleNo].SetActive (true);
		//CR.targetObject = vehicles [selectedvehicleNo].transform;
		//StartCoroutine (waitForMove());
	}

	IEnumerator waitForMove(){
		isVehicleChanged = true;
		yield return new WaitForSeconds (0.2f);
		isVehicleChanged = false;
	}

	public void OnRight(){
		if (!DisableChanging) {
			StartCoroutine (Right ());
			DisableChanging = true;
		}
	}

	IEnumerator Right(){
		if (selectedvehicleNo < vehicles.Length-1) {
			vehiclesDetails [selectedvehicleNo].GetComponent<Animator> ().SetTrigger ("Hide");
			yield return new WaitForSeconds (1.5f);
			vehicles [selectedvehicleNo].SetActive (false);
			vehiclesDetails [selectedvehicleNo].SetActive (false);
			selectedvehicleNo++;
			//yield return new WaitForSeconds (1);
			vehicles [selectedvehicleNo].SetActive (true);
			NameText.text = vehicles [selectedvehicleNo].name;
			vehiclesDetails [selectedvehicleNo].SetActive (true);
			ManagingScript.SetVehicleNo (selectedvehicleNo);
			//CR.targetObject = vehicles [selectedvehicleNo].transform;
			//StartCoroutine (waitForMove());
		}else{
			vehiclesDetails [selectedvehicleNo].GetComponent<Animator> ().SetTrigger ("Hide");
			yield return new WaitForSeconds (1.5f);
			vehicles [selectedvehicleNo].SetActive (false);
			vehiclesDetails [selectedvehicleNo].SetActive (false);
			selectedvehicleNo= 0;
			//yield return new WaitForSeconds (1);
			vehicles [selectedvehicleNo].SetActive (true);
			NameText.text = vehicles [selectedvehicleNo].name;
			vehiclesDetails [selectedvehicleNo].SetActive (true);
			ManagingScript.SetVehicleNo (selectedvehicleNo);
			//CR.targetObject = vehicles [selectedvehicleNo].transform;
			//StartCoroutine (waitForMove());
		}
		DisableChanging = false;
	}

	public void OnLeft(){
		if (!DisableChanging) {
			StartCoroutine (Left ());
			DisableChanging = true;
		}
	}

	IEnumerator Left(){
		if (selectedvehicleNo > 0) {
			vehiclesDetails [selectedvehicleNo].GetComponent<Animator> ().SetTrigger ("Hide");
			yield return new WaitForSeconds (1.5f);
			vehicles [selectedvehicleNo].SetActive (false);
			vehiclesDetails [selectedvehicleNo].SetActive (false);
			selectedvehicleNo--;
			//yield return new WaitForSeconds (1);
			vehicles [selectedvehicleNo].SetActive (true);
			NameText.text = vehicles [selectedvehicleNo].name;
			vehiclesDetails [selectedvehicleNo].SetActive (true);
			ManagingScript.SetVehicleNo (selectedvehicleNo);
			//CR.targetObject = vehicles [selectedvehicleNo].transform;
			//StartCoroutine (waitForMove());
		} else {
			vehiclesDetails [selectedvehicleNo].GetComponent<Animator> ().SetTrigger ("Hide");
			yield return new WaitForSeconds (1.5f);
			vehicles [selectedvehicleNo].SetActive (false);
			vehiclesDetails [selectedvehicleNo].SetActive (false);
			selectedvehicleNo = vehicles.Length-1;
			//yield return new WaitForSeconds (1);
			vehicles [selectedvehicleNo].SetActive (true);
			NameText.text = vehicles [selectedvehicleNo].name;
			vehiclesDetails [selectedvehicleNo].SetActive (true);
			ManagingScript.SetVehicleNo (selectedvehicleNo);
			//CR.targetObject = vehicles [selectedvehicleNo].transform;
			//StartCoroutine (waitForMove());
		}
		DisableChanging = false;
	}

	public void OnSelect(){
		VehicleDetails VD = vehiclesDetails [selectedvehicleNo].GetComponent<VehicleDetails> ();
		if (VD.isUnlocked == 0) {
			BuyVehiclePopup.SetActive (true);
		} else {
			StartCoroutine (endScreenCoroutine(3));
		}
	}

	public void OnBack(){
		StartCoroutine (endScreenCoroutine(1));
	}

	public void OnBuyClick(){
		VehicleDetails VD = vehiclesDetails [selectedvehicleNo].GetComponent<VehicleDetails> ();
		if (VD.price <= ManagingScript.Amount) {
			int NewPrice = ManagingScript.Amount - VD.price;
			ManagingScript.SetAmount (NewPrice);
			Amount.text = ManagingScript.Amount+"";
			VD.Unlock ();

		} else {
			NotEnoughMoneyPop.SetActive (true);
		}
	}

	public void OnCloseClick(GameObject obj){
		obj.SetActive(false);
	}

	IEnumerator endScreenCoroutine(int num){
		EndBlackScreen.SetActive (true);
		yield return new WaitForSeconds (1.2f);
		Application.LoadLevel (num);
		if (num == 4) {
			ManagingScript.isDecreaseVolume = true;
		}
	}

	public void AdsCompleted(){
		ManagingScript.Amount += Random.Range (150,200);
		ManagingScript.SetAmount (ManagingScript.Amount);
		Amount.text = ManagingScript.Amount+"";
	}
		
}
