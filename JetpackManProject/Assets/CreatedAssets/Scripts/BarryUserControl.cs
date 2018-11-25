using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof(BarryCharacter))]
    public class BarryUserControl : NetworkBehaviour
    {
        private BarryCharacter m_Character;
        private float fly;


        private void Awake()
        {
            
            m_Character = GetComponent<BarryCharacter>();
        }


        private void Update()
        {
            
                // Read the inputs.
                fly = Input.GetAxis("Vertical");
                //bool crouch = Input.GetKey(KeyCode.LeftControl);
                float h = Input.GetAxis("Horizontal");
                bool attack = Input.GetButtonDown("Fire1");

                // Pass all parameters to the character control script.
                m_Character.Move(h, attack, fly);
                fly = 0;
            
        }


        private void FixedUpdate()
        {
			

        }
    }
}
