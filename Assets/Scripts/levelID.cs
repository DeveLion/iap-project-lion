using UnityEngine;
using System.Collections;

public class levelID : MonoBehaviour {
	public int levelId;
	public int isLocked;
	public GameObject unlockImage;
	void Start(){
		//ManagingScript.TotalLevelCompleted = 25;
			if(levelId > ManagingScript.TotalLevelCompleted){
				unlockImage.SetActive (true);
				isLocked = 0;
			} else {
				unlockImage.SetActive (false);
				isLocked = 1;
			}
			
	}
}
