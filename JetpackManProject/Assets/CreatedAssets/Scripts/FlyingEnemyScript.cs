using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
namespace UnityStandardAssets._2D
{
    public class FlyingEnemyScript : NetworkBehaviour
    {
        Animator mAnim;
        GameObject[] players;
        [SerializeField] float moveSpeed = 7;
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
            players = GameObject.FindGameObjectsWithTag("Player");
        }

        // Update is called once per frame
        void Update()
        {

        }

        void Move()
        {

            GameObject player = getClosestPlayer(gameObject.transform);
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

                collision.gameObject.GetComponent<BarryCharacter>().TakeDamage();

            }
        }

        void Flip()
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        GameObject getClosestPlayer(Transform point)
        {
            double shortestDistance = double.MaxValue;
            GameObject closestObject = players[0];
            for (int i = 0; i < players.Length; i++)
            {
                double distance = System.Math.Pow(players[i].transform.position.x - point.position.x, 2) + System.Math.Pow(players[i].transform.position.y - point.position.y, 2);

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestObject = players[i];
                }
            }
            return closestObject;
        }
    }
}