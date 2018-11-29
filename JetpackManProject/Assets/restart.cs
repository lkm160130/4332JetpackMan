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
            

            GameManagerScript.partsCollected = 0;
            GameManagerScript.part1 = false;
            GameManagerScript.part2 = false;
            GameManagerScript.part3 = false;
            GameManagerScript.part4 = false;
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            for(int i = 0; i < players.Length; i++)
            {
                players[i].transform.position = Vector3.zero;
                players[i].GetComponent<BarryCharacter>().BarryHealth = 3;
                players[i].GetComponent<Animator>().SetBool("Dead", false);
                players[i].GetComponent<Animator>().SetBool("Dying", false);
                players[i].GetComponent<Animator>().SetBool("Gun", false);
            }
            //GameManagerScript.KillAllEnemies();
            //GameManagerScript.SpawnAllEnemies();
            GameObject.FindGameObjectWithTag("GameOver").GetComponent<Canvas>().enabled = false;

        }

    }
}

