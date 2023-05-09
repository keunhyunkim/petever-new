using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public Transform Target;
    public float speed = 3f;


    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Target.position, Vector3.up, speed * Time.deltaTime);
    }
}

