using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterHealingForest : MonoBehaviour
{
    public static bool isHFEntered = false;
    GameObject HealingForestStartPoint;

    public GameObject guidePrefab;

    // Start is called before the first frame update
    void Start()
    {
        HealingForestStartPoint = GameObject.Find("HealingForestStartPoint");
    }

    void showHealingForestGuide()
    {
        GameObject previousCanvas;

        previousCanvas = GameObject.FindGameObjectWithTag("HealingForestGuide");
        if (previousCanvas == null)
        {
            GameObject canvas = Instantiate(guidePrefab) as GameObject;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Owner")
        {
            if (isHFEntered == false) {
                //When try to enter Healing-Forest, move the position
                collision.transform.position = HealingForestStartPoint.transform.position;
                isHFEntered = true;

                showHealingForestGuide();
            }
        }

    }
}
