using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class cameraFollowScript : NetworkBehaviour {

    Vector3 vctr = new Vector3(0, 0, -10);
    [SerializeField] private int id;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        /* 
         for(int i = 0; i < 4; i++)
         {
             if(players[i].id == id)
             {
                 gameObject.transform.position = players[i].transform.position;

             }
         }
         */
        gameObject.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0,0,-10);
	}
}
