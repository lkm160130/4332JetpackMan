using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets._2D
{
    public class BarryCharacter : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;// The fastest the player can travel in the x axis.
        [SerializeField] private float m_MaxThrust = 15f;
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.

        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;

        [SerializeField] GameObject projectile;
		[SerializeField] GameObject jetpackFire;

         public int health = 100;
        
		private bool fly; 

         static public bool plain = true;
         static public bool gun = false;
         static public bool sword = false;

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .7f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        static private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        private void Awake()
        {
            Debug.Log("Test");
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }
		private void Start(){
			StartCoroutine (MakeJetpackFire ());
		}


        private void FixedUpdate()
        {
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }
            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

            if (!m_Anim.GetBool("Ground"))
            {
                //create thrust animation 
            }
        }


        public void Move(float move, bool attack, float fly)
        {
			if (fly > .1f)
				this.fly = true;
			else
				this.fly = false;

			if (!m_Anim.GetBool ("Dying") && !m_Anim.GetBool ("Dead")) {


				//only control the player if grounded or airControl is turned on
				if (m_Grounded || m_AirControl) {
					// Reduce the speed if crouching by the crouchSpeed multiplier


					// The Speed animator parameter is set to the absolute value of the horizontal input.
					m_Anim.SetFloat ("Speed", Mathf.Abs (move));

					// Move the character
					if (fly > 0.1) {
						m_Rigidbody2D.velocity = new Vector2 (move * m_MaxSpeed, fly * m_MaxThrust); //[THIS]
					} else {
						m_Rigidbody2D.velocity = new Vector2 (move * m_MaxSpeed, m_Rigidbody2D.velocity.y);
					}
					// If the input is moving the player right and the player is facing left...
					if (move > 0 && !m_FacingRight) {
						// ... flip the player.
						Flip ();
					}
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight) {
						// ... flip the player.
						Flip ();
					}
				}
				// If the player should jump...
				// if (/*m_Grounded &&*/ fly /*&& m_Anim.GetBool("Ground")*/)
				// {
				//     // Add a vertical force to the player.

				if (Math.Abs (fly) > 0.1)
					m_Grounded = false;
				//     m_Anim.SetBool("Ground", false);
				//     m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
				// }
				if (!gun && !sword) {
					plain = true;
					m_Anim.SetBool ("Plain", plain);
				}
				if (attack && gun) {
					StartCoroutine(fireGun ());
				}
				if (attack && sword) {
					StartCoroutine (SwingSword ());
               
				}
            
			} else {
				if (m_Grounded)
				m_Rigidbody2D.velocity = Vector2.zero;
			}
            
        }

        private IEnumerator SwingSword()
        {
            //Debug.Log("swing?");
            if (m_Anim.GetBool("Ground"))
            {
                 m_Anim.SetBool("SwingSword", true);
                 yield return new WaitForSeconds(.4f);
                Debug.Log("Delay?");
                m_Anim.SetBool("SwingSword", false);
            }
        }



        IEnumerator fireGun()
        {
            GameObject fired;
            if (!m_Anim.GetBool("FiringGun")) {
                m_Anim.SetBool("FiringGun", true);
            if (m_Anim.GetBool("Ground"))
            {
                if (m_FacingRight)
                    fired = Instantiate(projectile, new Vector3(GetComponent<Transform>().localPosition.x + 3, GetComponent<Transform>().localPosition.y - .2f), Quaternion.identity);
                else
                    fired = Instantiate(projectile, new Vector3(GetComponent<Transform>().localPosition.x - 3, GetComponent<Transform>().localPosition.y - .2f), Quaternion.identity);
                //fire in on ground position
            }
            else
            {
                if (m_FacingRight)
                    fired = Instantiate(projectile, new Vector3(GetComponent<Transform>().localPosition.x + 3, GetComponent<Transform>().localPosition.y - .4f), Quaternion.identity);
                else
                    fired = Instantiate(projectile, new Vector3(GetComponent<Transform>().localPosition.x - 3, GetComponent<Transform>().localPosition.y - .4f), Quaternion.identity);

            }
                yield return new WaitForSeconds(0.2f);
                m_Anim.SetBool("FiringGun", false);
         }
        }

        static public void setGunTrue()
        {
            gun = true;
            plain = false;
            sword = false;
            m_Anim.SetBool("Sword", false);
            m_Anim.SetBool("Gun", true);
            m_Anim.SetBool("Plain", false);
        }
        static public void setPlainTrue()
        {
            gun = false;
            plain = true;
            sword = false;
            m_Anim.SetBool("Sword", false);
            m_Anim.SetBool("Gun", false);
            m_Anim.SetBool("Plain", true);
        }
        static public void setSwordTrue()
        {
            gun = false;
            plain = false;
            sword = true;
            m_Anim.SetBool("Sword", true);
            m_Anim.SetBool("Gun", false);
            m_Anim.SetBool("Plain", false);
        }
        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        public void TakeDamage(int damage)
        {
            health = health - damage;
            Debug.Log("Ouch!");
            if (health <= 0)
            {
               
				StartCoroutine(DeathAnim ());
            }
        }
		IEnumerator DeathAnim()
		{
			m_Anim.SetBool("Dying", true);
			Debug.Log("Your Dead!!!");

			yield return new WaitForSeconds(.5f);
			m_Anim.SetBool ("Dying", false);
			m_Anim.SetBool ("Dead", true);
		}
		IEnumerator MakeJetpackFire()
		{
			while (true) {
				if (fly) {
					if(!m_FacingRight)
					Instantiate(jetpackFire, new Vector3(GetComponent<Transform>().localPosition.x + 1.3f, GetComponent<Transform>().localPosition.y - .3f), Quaternion.identity);
					else 
						Instantiate(jetpackFire, new Vector3(GetComponent<Transform>().localPosition.x - .2f, GetComponent<Transform>().localPosition.y - .3f), Quaternion.identity);
				}
				yield return new WaitForSeconds(.05f);

					
			}
		}
		void OnTriggerEnter2D(Collider2D collidedObject)
		{
			Debug.Log ("triggered!!!1");

			switch(collidedObject.name){
			case "cave_1":
				SceneManager.LoadScene ("cave_1");
				break;
			case "cave_2":

				break;
			case "cave_3":

				break;
			case "cave_4":

				break;
			case "cave_5":

				break;
			case "cave_6":

				break;
			case "cave_7":

				break;
			case "cave_8":
				
				break;
		}


    }
}
}
