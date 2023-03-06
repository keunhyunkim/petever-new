using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterHealingForest : MonoBehaviour
{
    public static bool isHFEntered = false;
    GameObject HealingForestStartPoint;
    // Start is called before the first frame update
    void Start()
    {
        HealingForestStartPoint = GameObject.Find("HealingForestStartPoint");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Owner")
        {
            if (isHFEntered == false) {
                //When try to enter Healing-Forest, move the position
                collision.transform.position = HealingForestStartPoint.transform.position;
                isHFEntered = true;
            }
        }

    }
}
