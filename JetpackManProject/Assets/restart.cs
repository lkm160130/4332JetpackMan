using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
namespace UnityStandardAssets._2D
{
    public class restart : NetworkBehaviour {
        [SerializeField] GameObject player;
        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        public void Restart()
        {
            NetworkManager.singleton.ServerChangeScene("OverWorld");

            GameManagerScript.partsCollected = 0;
            GameManagerScript.part1 = false;
            GameManagerScript.part2 = false;
            GameManagerScript.part3 = false;
            GameManagerScript.part4 = false;
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            
            GameObject.FindGameObjectWithTag("GameOver").GetComponent<Canvas>().enabled = false;

        }

    }
}

