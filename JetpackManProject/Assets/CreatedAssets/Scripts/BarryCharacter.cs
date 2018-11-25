using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

namespace UnityStandardAssets._2D
{
    public class BarryCharacter : NetworkBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;// The fastest the player can travel in the x axis.
        [SerializeField] private float m_MaxThrust = 15f;
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.

        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;

        [SerializeField] GameObject projectile;
        [SerializeField] GameObject jetpackFire;
        [SerializeField] float damageDelayTime = 3;

        public string currentScene = "OverWorld";

        Boolean canTakeDamage = true;

        int gameID;

        private bool fly;

 
        static public bool plain = true;

        static public bool gun = false;

        static public bool sword = false;

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .7f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        static private NetworkAnimator networkAnimator;
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        float currentX;
        float pastX;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            
           
           // Debug.Log("Test");
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = this.GetComponent<Animator>();
            networkAnimator = gameObject.GetComponent<NetworkAnimator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }
        private void Start()
        {
            
            currentX = transform.position.x;
            pastX = transform.position.x;
            StartCoroutine(MakeJetpackFire());
            
            Debug.Log("anim instance" + m_Anim.GetInstanceID());
        }

        private void Update()
        {
            if(GameManagerScript.scene != currentScene)
            {
                currentScene = GameManagerScript.scene;
                
                switch (currentScene)
                {
                    case "cave_1":
                        
                        gameObject.transform.position = new Vector3(5, 10, 0);
                        break;
                    case "cave_2":
                        
                        gameObject.transform.position = new Vector3(54, 58, 0);
                        break;
                    case "cave_3":
                        
                        gameObject.transform.position = new Vector3(81, 100, 0);
                        break;
                    case "cave_4":
                        
                        gameObject.transform.position = new Vector3(54, 58, 0);
                        break;
                    case "cave_5":

                        break;
                    case "cave_6":

                        break;
                    case "cave_7":

                        break;
                    case "cave_8":

                        break;
                    case "OverWorld1":
                        
                        gameObject.transform.position = new Vector3(-21, -7, 0);
                        break;
                    case "OverWorld2":
                        
                        gameObject.transform.position = new Vector3(111, -7, 0);
                        break;
                    case "OverWorld3":
                        
                        gameObject.transform.position = new Vector3(243, -7, 0);
                        break;
                    case "OverWorld4":
                        
                        gameObject.transform.position = new Vector3(-153, -7, 0);
                        break;

                }
            }
        }


        private void FixedUpdate()
        {
            pastX = currentX;
            currentX = transform.position.x;
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.tag == "Environment")
                    m_Grounded = true;
            }
            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

            if (!m_Anim.GetBool("Ground"))
            {
                //create thrust animation 
            }

           // Debug.Log(GameManagerScript.P1Health);
        }


        public void Move(float move, bool attack, float fly)
        {
            
            if (fly > .1f)
                this.fly = true;
            else
                this.fly = false;

            if (!networkAnimator.animator.GetBool("Dying") && !m_Anim.GetBool("Dead"))
            {


                //only control the player if grounded or airControl is turned on
                if (m_Grounded || m_AirControl)
                {
                    // Reduce the speed if crouching by the crouchSpeed multiplier


                    // The Speed animator parameter is set to the absolute value of the horizontal input.
                    networkAnimator.animator.SetFloat("Speed", Mathf.Abs(m_Rigidbody2D.velocity.x));

                    // Move the character
                    if (fly > 0.1)
                    {
                        
                            m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, fly * m_MaxThrust); //[THIS]
                    }
                    else
                    {
                        m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);
                    }
                    // If the input is moving the player right and the player is facing left...
                    if (currentX > pastX + .001 && !m_FacingRight)
                    {
                        // ... flip the player.
                        Flip();
                    }
                    // Otherwise if the input is moving the player left and the player is facing right...
                    else if (currentX + .001 < pastX && m_FacingRight)
                    {
                        // ... flip the player.
                        Flip();
                    }
                }
                // If the player should jump...
                // if (/*m_Grounded &&*/ fly /*&& m_Anim.GetBool("Ground")*/)
                // {
                //     // Add a vertical force to the player.

                if (Math.Abs(fly) > 0.1)
                    m_Grounded = false;
                //     m_Anim.SetBool("Ground", false);
                //     m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                // }
                if (!gun && !sword)
                {
                    plain = true;
                    networkAnimator.animator.SetBool("Plain", plain);
                }
                if (attack && gun)
                {
                    StartCoroutine(fireGun());
                }
                if (attack && sword)
                {
                    StartCoroutine(SwingSword());

                }

            }
            else
            {
                if (m_Grounded)
                    m_Rigidbody2D.velocity = Vector2.zero;
            }

        }

        private IEnumerator SwingSword()
        {
            //Debug.Log("swing?");
            if (networkAnimator.animator.GetBool("Ground"))
            {
                networkAnimator.animator.SetBool("SwingSword", true);
                yield return new WaitForSeconds(.4f);
                // Debug.Log("Delay?");
                networkAnimator.animator.SetBool("SwingSword", false);
            }
        }


        
        IEnumerator fireGun()
        {
            GameObject fired;
            if (!networkAnimator.animator.GetBool("FiringGun"))
            {
                m_Anim.SetBool("FiringGun", true);
                if (networkAnimator.animator.GetBool("Ground"))
                {
                    if (m_FacingRight)
                        createProjectile(3, -.2f);
                    else
                        createProjectile(-3, -.2f);
                    //fire in on ground position
                }
                else
                {
                    if (m_FacingRight)
                        createProjectile(3, -.4f);
                    else
                        createProjectile(-3, -.4f);

                }
                yield return new WaitForSeconds(0.2f);
                networkAnimator.animator.SetBool("FiringGun", false);
            }
        }

        
        public void createProjectile(float x, float y)
        {
           Instantiate(projectile, new Vector3(GetComponent<Transform>().localPosition.x + x, GetComponent<Transform>().localPosition.y + y), Quaternion.identity);
        }

        static public void setGunTrue()
        {
            gun = true;
            plain = false;
            sword = false;
            networkAnimator.animator.SetBool("Sword", false);
            networkAnimator.animator.SetBool("Gun", true);
            networkAnimator.animator.SetBool("Plain", false);
        }
        static public void setPlainTrue()
        {
            gun = false;
            plain = true;
            sword = false;
            networkAnimator.animator.SetBool("Sword", false);
            networkAnimator.animator.SetBool("Gun", false);
            networkAnimator.animator.SetBool("Plain", true);
        }
        static public void setSwordTrue()
        {
            gun = false;
            plain = false;
            sword = true;
            networkAnimator.animator.SetBool("Sword", true);
            networkAnimator.animator.SetBool("Gun", false);
            networkAnimator.animator.SetBool("Plain", false);
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
        public void TakeDamage()
        {
            int health;
            switch (1) {
                case 1:
                    if (canTakeDamage)
                    {
                        canTakeDamage = false;
                        GameManagerScript.P1Health--;
                        health = GameManagerScript.P1Health;
                        if (health <= 0)
                        {

                            StartCoroutine(DeathAnim());
                        }
                        StartCoroutine(DamageDelay(damageDelayTime));
                    }
            
            
                   
                    break;

                case 2:
                    if (canTakeDamage)
                    {
                        canTakeDamage = false;
                        GameManagerScript.P2Health--;
                        health = GameManagerScript.P1Health;
                        if (health <= 0)
                        {

                            StartCoroutine(DeathAnim());
                        }
                        StartCoroutine(DamageDelay(damageDelayTime));
                    }

                case 3:
                    if (canTakeDamage)
                    {
                        canTakeDamage = false;
                        GameManagerScript.P3Health--;
                        health = GameManagerScript.P1Health;
                        if (health <= 0)
                        {

                            StartCoroutine(DeathAnim());
                        }
                        StartCoroutine(DamageDelay(damageDelayTime));
                    }
                case 4:
                    if (canTakeDamage)
                    {
                        canTakeDamage = false;
                        GameManagerScript.P4Health--;
                        health = GameManagerScript.P1Health;
                        if (health <= 0)
                        {

                            StartCoroutine(DeathAnim());
                        }
                        StartCoroutine(DamageDelay(damageDelayTime));
                    }

            }
          
            Debug.Log("Ouch!");
            
        }
        IEnumerator DeathAnim()
        {
            networkAnimator.animator.SetBool("Dying", true);
            //Debug.Log("Your Dead!!!");

            yield return new WaitForSeconds(.5f);
            networkAnimator.animator.SetBool("Dying", false);
            networkAnimator.animator.SetBool("Dead", true);
        }
        IEnumerator MakeJetpackFire()
        {
            while (true)
            {
                if (fly)
                {
                    if (!m_FacingRight)
                        Instantiate(jetpackFire, new Vector3(GetComponent<Transform>().localPosition.x + 1.3f, GetComponent<Transform>().localPosition.y - .3f), Quaternion.identity);
                    else
                        Instantiate(jetpackFire, new Vector3(GetComponent<Transform>().localPosition.x - .2f, GetComponent<Transform>().localPosition.y - .3f), Quaternion.identity);
                }
                yield return new WaitForSeconds(.05f);


            }
        }
        void OnTriggerEnter2D(Collider2D collidedObject)
        {
           // Debug.Log("triggered!!!1");

            switch (collidedObject.name)
            {
                case "cave_1":
                    NetworkManager.singleton.ServerChangeScene("cave_1");
                    GameManagerScript.scene = "cave_1";
                    break;
                case "cave_2":
                    NetworkManager.singleton.ServerChangeScene("cave_2");
                    GameManagerScript.scene = "cave_1";
                    break;
                case "cave_3":
                    NetworkManager.singleton.ServerChangeScene("cave_3");
                    GameManagerScript.scene = "cave_1";
                    break;
                case "cave_4":
                    NetworkManager.singleton.ServerChangeScene("cave_4");
                    GameManagerScript.scene = "cave_1";
                    break;
                case "cave_5":

                    break;
                case "cave_6":

                    break;
                case "cave_7":

                    break;
                case "cave_8":

                    break;
                case "ExitCave1":
                    NetworkManager.singleton.ServerChangeScene("OverWorld");
                    GameManagerScript.scene = "OverWorld1";
                    break;
                case "ExitCave2":
                    NetworkManager.singleton.ServerChangeScene("OverWorld");
                    GameManagerScript.scene = "OverWorld2";
                    break;
                case "ExitCave3":
                    NetworkManager.singleton.ServerChangeScene("OverWorld");
                    GameManagerScript.scene = "OverWorld3";
                    break;
                case "ExitCave4":
                    NetworkManager.singleton.ServerChangeScene("OverWorld");
                    GameManagerScript.scene = "OverWorld4";
                    break;
                    
            }


        }
        IEnumerator DamageDelay(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
        canTakeDamage = true;
        }
    }
}