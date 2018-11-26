using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart1 : MonoBehaviour {
    [SerializeField] Sprite newImg;
    [SerializeField] int changeNum;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        int playerHealth = GameManagerScript.P1Health;
        if(playerHealth < changeNum)
        {

            GetComponent<Image>().sprite = newImg;
        }
    }
}
