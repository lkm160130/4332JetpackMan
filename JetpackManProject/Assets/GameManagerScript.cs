using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManagerScript : NetworkBehaviour
{
    [SerializeField] public static int P1Health = 3;
    [SerializeField] public static int P2Health = 3;
    [SerializeField] public static int P3Health = 3;
    [SerializeField] public static int P4Health = 3;

    public static string scene = "OverWorld";

    public static int partsCollected = 0;
    static bool part1 = false;
    static bool part2 = false;
    static bool part3 = false;
    static bool part4 = false;

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

    // Update is called once per frame
    void Update() {

    }

    



   
}
