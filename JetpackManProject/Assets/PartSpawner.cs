using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PartSpawner : NetworkBehaviour {

    [SerializeField] public GameObject part;
    [SerializeField] public float x;
    [SerializeField] public float y;
    [SerializeField] public string thisPartName = "part1";

    // Use this for initialization
    void Start () {
        
    }

    private void Awake()
    {
        //spawnPart();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void spawnPart()
    {
        Debug.Log("SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSs");
        if (!GameManagerScript.isPartCollected(thisPartName)) { 
        GameObject o = Instantiate(part, transform.position, Quaternion.identity);
        NetworkServer.Spawn(o);
    }
 
    }
}
