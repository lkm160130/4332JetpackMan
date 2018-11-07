using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets._2D
{
public class TentacleScript : MonoBehaviour {

		Animator mAnim;
		[SerializeField] int damage = 1;

	// Use this for initialization
	void Start () {
			mAnim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{

			Debug.Log("Triggered");
			GameObject.Find("Barry").GetComponent<BarryCharacter>().TakeDamage(damage);

		}
	}
}
}
