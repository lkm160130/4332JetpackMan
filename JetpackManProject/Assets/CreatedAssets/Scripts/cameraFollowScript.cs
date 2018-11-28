using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace UnityStandardAssets._2D
{
    public class cameraFollowScript : MonoBehaviour {

        Vector3 vctr = new Vector3(0, 0, -10);
        [SerializeField] private int id;

        // Use this for initialization
        void Start() {

        }
        private void Awake()
        {
            //GetComponent<Camera>().enabled = false;
           // DontDestroyOnLoad(gameObject);
            
        }

        // Update is called once per frame
        void Update() {
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
            if (GetComponentInParent<BarryCharacter>().isLocalPlayer)
            {
                enabled = true;
                GetComponent<Camera>().enabled = true;
            }
            gameObject.transform.position = transform.parent.gameObject.transform.position + new Vector3(0, 0, -10);
        }
    }
}
