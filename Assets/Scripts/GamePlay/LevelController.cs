using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {
	public GameObject [] Players;
	public GameObject Player;
	public GameObject[] Bosses;
	public static bool IsNext;
	int pointNo = 0;
	int selectedPoint;
	int TotalNo;
	public Text levelText,detailText;
	public GameObject LevelImage,endblackscreen;
	public static bool isGameWin;
	public static bool IsBossDone = false, isBossOn = false;
	public Text EnemyName;
	public Text PlayerName;
	public GameObject MainCam;
	public GameObject BossOn;
	// Use this for initialization
	void Start () {
		if (ManagingScript.LevelLoaded == 0) {
			ManagingScript.LevelLoaded = 1;
		}
		Player = Players [ManagingScript.VehicleNo];
		MainCam.transform.SetParent (Player.transform.FindChild ("CamPos").transform);
		MainCam.transform.localPosition = Vector3.zero;
		MainCam.transform.localRotation = Quaternion.identity;
		Player.SetActive (true);
		PlayerName.text = Player.name;
		TotalNo = ManagingScript.LevelLoaded*2;
		pointNo = 1;
		print (TotalNo+"  "+pointNo);
		LevelStartCouroutine();

	}

	public void AnimalName(string EnemyN){
		EnemyName.text = EnemyN+"";
	}

	void LevelStartCouroutine(){
		levelText.text = "STAGE : "+ManagingScript.LevelLoaded;
		detailText.text = "KILL "+TotalNo+" ANIMALS TO AWAKE THE BOSS, FIND HIM IN CASTLE AND KILL HIM";
		LevelImage.SetActive (true);
	}

	// Update is called once per frame
	void Update () {

		if (IsNext) {
			IsNext = false;
			if (pointNo < TotalNo) {
				pointNo++;
				TimeController.isPicked = true;
				print (TotalNo + "  " + pointNo);
			} else {
				
				if (!isBossOn) {
					TimeController.isPicked = true;
					StartCoroutine (BossBlink());
					isBossOn = true;
					print ("JINN");
					Bosses [ManagingScript.LevelLoaded-1].SetActive (true);
				}
			}
		}
			
		if (IsBossDone) {
			print ("level complete");
			//ManagingScript.lastLevelCompleted = true;
			ManagingScript.levelCleared = true;
			isBossOn = false;
			IsBossDone = false;
			isGameWin = true;
			TimeController.IsBoss = true;
			//print ("PointPC"+ManagingScript.levelCleared);
			ManagingScript.onLevelCleared ();
			ManagingScript.Amount += TimeController.totalEarning;
			ManagingScript.SetAmount (ManagingScript.Amount);

			StartCoroutine (Wait ());
		}

		if (Player.GetComponent<AnimalCharacterController> ().isDead) {
			StartCoroutine (WaitForDeath());
			print ("Worked");
		}

	}


	IEnumerator BossBlink(){
		BossOn.SetActive (!BossOn.activeSelf);
		yield return new WaitForSeconds (1.5f);
		if(!IsBossDone)
		StartCoroutine (BossBlink());
	}


	IEnumerator WaitForDeath(){
		ManagingScript.levelCleared = false;
		yield return new WaitForSeconds (0.5f);
		StartCoroutine (BlackScreenCouroutine(5));
	}


	IEnumerator Wait(){
		yield return new WaitForSeconds (4f);
		StartCoroutine (BlackScreenCouroutine(5));
	}

	IEnumerator BlackScreenCouroutine(int level){
		endblackscreen.SetActive (true);
		yield return new WaitForSeconds (1f);
		Application.LoadLevel (level);
	}

}
