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
    





    static int partsCollected = 0;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    



   
}
