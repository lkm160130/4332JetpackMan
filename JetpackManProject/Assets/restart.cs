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

           
            
            GameManagerScript.part1 = false;
            GameManagerScript.part2 = false;
            GameManagerScript.part3 = false;
            GameManagerScript.part4 = false;
            //GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>().Begun = false;
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>().partsCollected = 0;


            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            GameObject[] partSpawners = GameObject.FindGameObjectsWithTag("PartSpawner");

            for(int j = 0; j < partSpawners.Length; j++)
            {
                partSpawners[j].GetComponent<PartSpawner>().spawnPart();
            }
           
            for(int i = 0; i < players.Length; i++)
            {
                players[i].GetComponent<BarryCharacter>().resetPosition();
                players[i].GetComponent<BarryCharacter>().BarryHealth = 3;
                players[i].GetComponent<BarryCharacter>().gun = false;
                players[i].GetComponent<Animator>().SetBool("Dead", false);
                players[i].GetComponent<Animator>().SetBool("Dying", false);
                players[i].GetComponent<Animator>().SetBool("Gun", false);
                players[i].GetComponent<Animator>().SetBool("Plain", true);
                
            }
            GameManagerScript.KillAllEnemies();
            GameManagerScript.SpawnAllEnemies();
            GameManagerScript.DestroyParts();
            GameManagerScript.SpawnParts();
            GameObject.FindGameObjectWithTag("GameOver").GetComponent<Canvas>().enabled = false;
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>().CmdDisableWinScreen();


        }

    }
}

