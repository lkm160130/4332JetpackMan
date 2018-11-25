using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
namespace UnityStandardAssets._2D
{ 
public class SwordScript : NetworkBehaviour
    {

    // Use this for initialization
    void OnTriggerEnter2D(Collider2D collidedObject)
    {

        if (collidedObject.tag == "Player")
        {
            BarryCharacter.setSwordTrue();

            Destroy(gameObject);
        }
    }
}
}
