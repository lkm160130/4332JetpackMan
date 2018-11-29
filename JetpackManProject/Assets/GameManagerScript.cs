using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManagerScript : NetworkBehaviour
{
    

    
    
    public static int partsCollected = 0;

    public static bool part1 = false;
    public static bool part2 = false;
    public static bool part3 = false;
    public static bool part4 = false;

    [SyncVar]
    public bool Begun = false;

    public static bool isPartCollected(string partName)
    {

        switch (partName)
        {
            case "part1":
                return part1;
                
            case "part2":
                return part2;
                
            case "part3":
                return part3;
               
            case "part4":
                return part4;
                
        }
        return false;
    }

    public static void registerPartCollected(string partName)
    {
        switch (partName)
        {
            case "part1":
                part1 = true;
                break;
            case "part2":
                part2 = true;
                break;
            case "part3":
                part3 = true;
                break;
            case "part4":
                part4 = true;
                break;
        }
    }




    
    // Use this for initialization
    void Start() {
        
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if(Begun == false)
        {
            SpawnAllEnemies();
            Begun = true;
        }
    }

    public static void Begin()
    {
        
    }
    
    public static void  SpawnAllEnemies()
    {
        GameObject[] s = GameObject.FindGameObjectsWithTag("EnemySpawner");
        for(int i = 0; i < s.Length; i++) { 
        
            s[i].GetComponent<EnemySpawner>().Spawn();
        }
    }
    public static void KillAllEnemies()
    {
       GameObject[] e = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < e.Length; i++)
        {
            Destroy(e[i]);
        }
    }

    



   
}
