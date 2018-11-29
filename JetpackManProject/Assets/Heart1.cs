using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UnityStandardAssets._2D
{
    public class Heart1 : MonoBehaviour
    {
        [SerializeField] Sprite newImg;
        [SerializeField] Sprite startImg;
        [SerializeField] int changeNum;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            int playerHealth = transform.parent.gameObject.transform.parent.gameObject.GetComponent<BarryCharacter>().BarryHealth;
            if (playerHealth < changeNum)
            {

                GetComponent<Image>().sprite = newImg;
            }
            else
            {
                GetComponent<Image>().sprite = startImg;
            }
        }
    }
}