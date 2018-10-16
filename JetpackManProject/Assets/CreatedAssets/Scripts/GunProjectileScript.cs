using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets._2D {
    public class GunProjectileScript : MonoBehaviour {
        Rigidbody2D rBody;

        [SerializeField] float projectileSpeed = .05f;
        
        // Use this for initialization
        void Start()
        {

            rBody = GetComponent<Rigidbody2D>();
            if (GetComponent<Transform>().position.x < GameObject.Find("Barry").transform.position.x) { 
            rBody.velocity = new Vector2(-projectileSpeed, 0);
                Debug.Log(GetComponent<Transform>().position.x + " < " + GameObject.Find("Barry").transform.position.x);
            Flip();
            }
            else
            {
                rBody.velocity = new Vector2(projectileSpeed, 0);
                Debug.Log(GetComponent<Transform>().position.x + " < " + GameObject.Find("Barry").transform.position.x);
            }
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
        public void Flip()
        {
            // Switch the way the player is labelled as facing.
            

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}