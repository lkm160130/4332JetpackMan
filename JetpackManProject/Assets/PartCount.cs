using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartCount : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        int partCount = GameManagerScript.partsCollected;
        GetComponent<Text>().text = partCount.ToString();
	}
}
