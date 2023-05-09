using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitHealingForest : MonoBehaviour
{
    GameObject VillageBackPoint;
    // Start is called before the first frame update
    void Start()
    {
        VillageBackPoint = GameObject.Find("VillageBackPoint");
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Owner")
        {
            // if (EnterHealingForest.isHFEntered == true)
            // {
                collision.transform.position = VillageBackPoint.transform.position;
                // EnterHealingForest.isHFEntered = false;
            // }
        }

    }
}
