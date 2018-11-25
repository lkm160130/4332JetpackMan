using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
namespace UnityStandardAssets._2D
{
    public class FlyingEnemyScript : NetworkBehaviour
    {
        Animator mAnim;
        GameObject player;
        [SerializeField] float moveSpeed = 5;
        bool facingLeft = true;
        int noticeDistance;
        [SerializeField] int aggresiveDistance = 50;
        [SerializeField] int paciveDistance = 10;
        [SerializeField] int damage = 1;

        // Use this for initialization
        void Start()
        {
            noticeDistance = paciveDistance;
            mAnim = GetComponent<Animator>();
            player = GameObject.Find("Barry");
        }

        // Update is called once per frame
        void Update()
        {

        }

        void Move()
        {
            if (Vector2.Distance(player.transform.position, gameObject.transform.position) < noticeDistance)
            {
                noticeDistance = aggresiveDistance;
                GetComponent<Rigidbody2D>().velocity = new Vector2(player.transform.position.x - gameObject.transform.position.x,
                    player.transform.position.y - gameObject.transform.position.y).normalized * moveSpeed;
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                noticeDistance = paciveDistance;
            }

            if (player.transform.position.x > gameObject.transform.position.x && facingLeft)
            {
                Flip();
                facingLeft = false;
            }
            else if (player.transform.position.x < gameObject.transform.position.x && !facingLeft)
            {
                Flip();
                facingLeft = true;
            }
        }

        private void FixedUpdate()
        {
            Move();
            if (mAnim.GetBool("Attack") == true)
            {
                mAnim.SetBool("Attack", false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            
            if (collision.gameObject.tag == "Player")
            {
                mAnim.SetBool("Attack", true);
                
                GameObject.Find("Barry").GetComponent<BarryCharacter>().TakeDamage();

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