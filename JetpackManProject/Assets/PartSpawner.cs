using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartSpawner : MonoBehaviour {

    [SerializeField] public GameObject part;
    [SerializeField] public float x;
    [SerializeField] public float y;
    [SerializeField] public string thisPartName = "part1";

    // Use this for initialization
    void Start () {
        
    }

    private void Awake()
    {
        part.GetComponent<ShipPart>().partName = thisPartName;
        if(!GameManagerScript.isPartCollected(part.GetComponent<ShipPart>().partName))
        Instantiate(part, new Vector3(x, y), Quaternion.identity);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
