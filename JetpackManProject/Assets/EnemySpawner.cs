using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour{

    [SerializeField] GameObject enemy;

    
	public void CmdSpawn()
    {
        GameObject o = Instantiate(enemy, gameObject.transform.position, Quaternion.identity);
        NetworkServer.Spawn(o);
    }

    public void Spawn()
    {
        CmdSpawn();
    }
}
