using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


namespace UnityStandardAssets._2D {
    public class GunProjectileScript : NetworkBehaviour
    {
        Rigidbody2D rBody;
        public GameObject shooter;

        [SerializeField] float projectileSpeed = .05f;

        // Use this for initialization










        private void Start()
        {
            //hello



            //hello
            rBody = GetComponent<Rigidbody2D>();
            if (GetComponent<Transform>().position.x < shooter.transform.position.x) {
                rBody.velocity = new Vector2(-projectileSpeed, 0);

                Flip();
            }
            else
            {
                rBody.velocity = new Vector2(projectileSpeed, 0);

            }
            StartCoroutine(ProjectileTimeOut());
        }





        // Update is called once per frame
        void Update() {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Destroy(collision.gameObject); //replace with deal damage method
                Destroy(gameObject);
            }
            else if (collision.gameObject.tag == "Environment")
            {
                Destroy(gameObject);
            }

        }



        // Switch the way the player is labelled as facing.
        public void Flip() { 

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
            transform.localScale = theScale;
        
            }
		IEnumerator ProjectileTimeOut()
		{
			

			yield return new WaitForSeconds(5);
			Destroy (gameObject);
		}
    }
}