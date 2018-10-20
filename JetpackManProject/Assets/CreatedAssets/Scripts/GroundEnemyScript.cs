using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets._2D
{
    public class GroundEnemyScript : MonoBehaviour
    {
        Animator mAnim;
        GameObject player;
        [SerializeField] float moveSpeed = 5;
        bool facingLeft = true;
        int noticeDistance;
        [SerializeField] int aggresiveDistance = 50;
        [SerializeField] int paciveDistance = 10;
        [SerializeField] int damage = 1;

        private bool cantGoLeft = false;
        private bool cantGoRight = false;

        // Use this for initialization
        void Start()
        {
            noticeDistance = paciveDistance;
            mAnim = GetComponent<Animator>();
            player = GameObject.Find("Barry");
            Rigidbody2D rbod = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void Move()
        {

            mAnim.SetFloat("Speed", GetComponent<Rigidbody2D>().velocity.magnitude);
            if(Vector2.Distance(player.transform.position, gameObject.transform.position) < noticeDistance)
            {

                noticeDistance = aggresiveDistance;
                GetComponent<Rigidbody2D>().velocity = new Vector2((player.transform.position.x - gameObject.transform.position.x > 0) ? 
                    (cantGoRight) ? 0: moveSpeed : (cantGoLeft) ? 0: -moveSpeed , GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                noticeDistance = paciveDistance;
            }

            if(player.transform.position.x > gameObject.transform.position.x && facingLeft)
            {
                Flip();
                facingLeft = false;
            }
            else if(player.transform.position.x < gameObject.transform.position.x && !facingLeft)
            {
                Flip();
                facingLeft = true;
            }
        }

        private void FixedUpdate()
        {
            Move();
            if(mAnim.GetBool("Attack") == true)
            {
                mAnim.SetBool("Attack", false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                mAnim.SetBool("Attack", true);
                Debug.Log("Triggered");
                GameObject.Find("Barry").GetComponent<BarryCharacter>().TakeDamage(damage);
                
            }
            if(collision.gameObject.tag == "BoundaryCollider")
            {
                Debug.Log("boundary");
                if (facingLeft)
                {
                    cantGoLeft = true;
                    cantGoRight = false;
                }
                if (!facingLeft)
                {
                    cantGoRight = true;
                    cantGoLeft = false;
                }
               
            }
            else
            {
                cantGoLeft = false;
                cantGoRight = false;
            }
             if(collision.gameObject.tag == "Environment")
            {
                cantGoRight = false;
                cantGoLeft = false;
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                mAnim.SetBool("Attack", true);
                Debug.Log("Triggered");
                GameObject.Find("Barry").GetComponent<BarryCharacter>().TakeDamage(damage);

            }
            if (collision.gameObject.tag == "Environment")
            {
                cantGoRight = false;
                cantGoLeft = false;
            }
        }

        void Flip()
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}