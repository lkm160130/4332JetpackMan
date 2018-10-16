using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets._2D
{
    public class GunScript : MonoBehaviour
    {
        

        // Use this for initialization
        void OnTriggerEnter2D(Collider2D collidedObject)
        {
            
            if (collidedObject.tag == "Player")
            {
                BarryCharacter.setGunTrue();
               
                Destroy(gameObject);
            }
        }
    }
}