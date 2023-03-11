using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasLookCamera : MonoBehaviour
{
    private Camera cameraToLookAt;

    void Start () 
    {
        cameraToLookAt = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
    }


// Update is called once per frame

    void Update () 
    {
        transform.LookAt (transform.position + cameraToLookAt.transform.rotation * Vector3.back, cameraToLookAt.transform.rotation * Vector3.down);
    }
}