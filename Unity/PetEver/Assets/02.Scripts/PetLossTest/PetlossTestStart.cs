using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetlossTestStart : MonoBehaviour
{
    Vector3 StartPoint;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Owner")
        {
            StartPoint = GameObject.Find("TestPosition").transform.position;
            collision.transform.position = StartPoint;
        }

    }
}
