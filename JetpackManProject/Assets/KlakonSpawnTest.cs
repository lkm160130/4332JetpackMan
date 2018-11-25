using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class KlakonSpawnTest : NetworkBehaviour
{
    public GameObject enemyPrefab;
    public int numberOfEnemies;
    public Vector3[] enemySpawnPositions;
    private GameObject[] enemies;
    private int nPlayers = 0;

    public override void OnStartServer()
    {
        enemies = new GameObject[numberOfEnemies];
        for (int i = 0; i < numberOfEnemies; i++)
        {
            enemies[i] = Instantiate(enemyPrefab, enemySpawnPositions[i], Quaternion.identity);
            NetworkServer.Spawn(enemies[i]);
        }
    }

    private void Update()
    {
       
    }
}

