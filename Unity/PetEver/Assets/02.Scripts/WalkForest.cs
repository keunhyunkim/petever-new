using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;

public class WalkForest : MonoBehaviour
{
    GameObject forestLand;
    GameObject manCharater;
    Vector3 centerPos;
    // Start is called before the first frame update
    void Start()
    {
        forestLand = GameObject.Find("forestLand");
        Input.gyro.enabled = true;
        PlayerController.isForest = true;
        manCharater = GameObject.FindGameObjectWithTag("Owner");
    }

    // Update is called once per frame
    void Update()
    {
        // Physics.gravity = centerPos - manCharater.transform.position;
        //transform.Rotate(Input.gyro.rotationRateUnbiased.x, Input.gyro.rotationRateUnbiased.y, Input.gyro.rotationRateUnbiased.z);
        // UnityEngine.Debug.Log("x: " + Input.gyro.rotationRateUnbiased.x + " y: " + Input.gyro.rotationRateUnbiased.y + " z: " + Input.gyro.rotationRateUnbiased.z);
        if (Math.Abs(Input.gyro.rotationRateUnbiased.x) > 0.3f && Math.Abs(Input.gyro.rotationRateUnbiased.y) > 0.3f && Math.Abs(Input.gyro.rotationRateUnbiased.z) > 0.3f) {
            PlayerController.mov_x = 180;
            PlayerController.mov_y = 10;
            PlayerController.playerSpeed = 0.03f;
        } else {
            PlayerController.mov_x = 0;
            PlayerController.mov_y = 0;
        }
    }
}
