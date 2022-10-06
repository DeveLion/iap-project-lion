using UnityEngine;
using System.Collections;

public class DragonWalkingarround : MonoBehaviour {
	public GameObject[] objects;
	GameObject objecToFollow;
	int no;
	public float Speed = 45,RotateSpeed = 1;
	// Use this for initialization
	void Start () {
		no = 0;
		objecToFollow = objects[no];
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate (Vector3.forward * Time.deltaTime * Speed );
		//this.transform.LookAt (objecToFollow.transform.position);
		float distance = Vector3.Distance (transform.position,objecToFollow.transform.position);
		Vector3 targetDir = objecToFollow.transform.position - transform.position;
		float step = RotateSpeed * Time.deltaTime;
		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
		transform.rotation = Quaternion.LookRotation(newDir);
		if (distance < 5f) {
			no ++;
			if(no == 4)
			no = 0;
			objecToFollow = objects[no];
		}
	}
}
