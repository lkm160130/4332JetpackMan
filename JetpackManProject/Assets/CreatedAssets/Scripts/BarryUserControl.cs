using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof(BarryCharacter))]
    public class BarryUserControl : MonoBehaviour
    {
        private BarryCharacter m_Character;
        private float fly;


        private void Awake()
        {
            
            m_Character = GetComponent<BarryCharacter>();
        }


        private void Update()
        {
            // if (!m_Jump)
            //{
            // Read the jump input in Update so button presses aren't missed.

            // }
        }


        private void FixedUpdate()
        {
			
            // Read the inputs.
            fly = CrossPlatformInputManager.GetAxis("Vertical");
            //bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            bool attack = CrossPlatformInputManager.GetButtonDown("Fire1");
            
            // Pass all parameters to the character control script.
            m_Character.Move(h, attack,  fly);
            fly = 0;

        }
    }
}
