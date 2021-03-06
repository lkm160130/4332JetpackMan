using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace UnityStandardAssets._2D{
public class JetpackFireScript : NetworkBehaviour {

	// Use this for initialization
	private void Start () {
			StartCoroutine(FireTimeOut());
			GetComponent<Rigidbody2D>().velocity = new Vector2 (0, -1);
	}
	

	IEnumerator FireTimeOut()
	{
		yield return new WaitForSeconds(.4f);
		Destroy (gameObject); 
	}
}
}