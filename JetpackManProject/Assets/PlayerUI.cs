using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
namespace UnityStandardAssets._2D
{
    public class PlayerUI : NetworkBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        private void Awake()
        {
            // DontDestroyOnLoad(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            if (GetComponentInParent<BarryCharacter>().isLocalPlayer)
            {
                
                GetComponent<Canvas>().enabled = true;
            }

        }
    }
}
