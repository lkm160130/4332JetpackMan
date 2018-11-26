using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

namespace UnityStandardAssets._2D
{
    public class GunScript : NetworkBehaviour
    {
        

        // Use this for initialization
        void OnTriggerEnter2D(Collider2D collidedObject)
        {
            
            if (collidedObject.tag == "Player")
            {
                collidedObject.gameObject.GetComponent<BarryCharacter>().setGunTrue();

                Destroy(gameObject);
            }
        }
    }
}