using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPart : MonoBehaviour {

    [SerializeField] public string partName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       
        

    }

    void OnTriggerEnter2D(Collider2D collidedObject)
    {

        if (collidedObject.tag == "Player")
        {
            GameManagerScript.partsCollected++;
            GameManagerScript.registerPartCollected(partName);

            Destroy(gameObject);
        }
    }
}
