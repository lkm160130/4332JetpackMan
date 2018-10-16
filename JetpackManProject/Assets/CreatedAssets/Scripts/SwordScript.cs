using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets._2D { 
public class SwordScript : MonoBehaviour {

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
