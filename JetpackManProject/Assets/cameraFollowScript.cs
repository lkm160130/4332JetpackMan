using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollowScript : MonoBehaviour {

    Vector3 vctr = new Vector3(0, 0, -10);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = GameObject.Find("Barry").transform.position + vctr;
	}
}
