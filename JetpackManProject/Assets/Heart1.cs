using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UnityStandardAssets._2D
{
    public class Heart1 : MonoBehaviour
    {
        [SerializeField] Sprite newImg;
        [SerializeField] int changeNum;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log(transform.parent.gameObject.transform.parent.gameObject.GetComponent<BarryCharacter>().BarryHealth);
            int playerHealth = transform.parent.gameObject.transform.parent.gameObject.GetComponent<BarryCharacter>().BarryHealth;
            if (playerHealth < changeNum)
            {

                GetComponent<Image>().sprite = newImg;
            }
        }
    }
}