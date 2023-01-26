using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
using UnityEngine.UI;

using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Android;
using TMPro;

public class WalkForest : MonoBehaviour
{
    GameObject forestLand;
    GameObject manCharacter;
    Vector3 centerPos;
    Transform manLookAt;
    Transform forestMainCam_tr;

    public Transform Target;
    public float forestWalkSpeed = 0.8f;

    int steps;

    void Start()
    {
        forestLand = GameObject.Find("forestLand");

        Input.gyro.enabled = true;
        PlayerController.isForest = true;
        manCharacter = GameObject.FindGameObjectWithTag("Owner");
        Target = GameObject.Find("WalkLine").transform;
        manLookAt = GameObject.Find("ManLookAt").transform;
        forestMainCam_tr = GameObject.Find("ForestMainCam").transform;

        InputSystem.AddDevice<StepCounter>();
        if (StepCounter.current != null) {
            InputSystem.EnableDevice(StepCounter.current);
            StepCounter.current.MakeCurrent();
        }
        InputSystem.AddDevice<Accelerometer>();
        if (Accelerometer.current != null) {
            InputSystem.EnableDevice(Accelerometer.current);
        }
        steps = 0;
    }

    void WalkRound() {
        manCharacter.transform.LookAt(manLookAt);
        manLookAt.transform.RotateAround(Target.position, Vector3.up, forestWalkSpeed * Time.deltaTime);
        manCharacter.transform.RotateAround(Target.position, Vector3.up, forestWalkSpeed * Time.deltaTime);
        forestMainCam_tr.RotateAround(Target.position, Vector3.up, forestWalkSpeed * Time.deltaTime);
        PlayerController.playerAnimator.SetFloat("walking", forestWalkSpeed);
    }

    void Update()
    {
        if (StepCounter.current != null && StepCounter.current.enabled) {
            if (StepCounter.current.stepCounter.ReadValue() > steps) {
                steps = StepCounter.current.stepCounter.ReadValue();   
            }
        } else {
            print("No step counter found!");
        }

        WalkRound();

        //transform.Rotate(Input.gyro.rotationRateUnbiased.x, Input.gyro.rotationRateUnbiased.y, Input.gyro.rotationRateUnbiased.z);
        // UnityEngine.Debug.Log("x: " + Input.gyro.rotationRateUnbiased.x + " y: " + Input.gyro.rotationRateUnbiased.y + " z: " + Input.gyro.rotationRateUnbiased.z);
        if (Math.Abs(Input.gyro.rotationRateUnbiased.x) > 0.3f && Math.Abs(Input.gyro.rotationRateUnbiased.y) > 0.3f && Math.Abs(Input.gyro.rotationRateUnbiased.z) > 0.3f) {
            PlayerController.mov_x = 0;
            PlayerController.mov_y = 0;
            PlayerController.playerSpeed = 0;
        } else {
            PlayerController.mov_x = 0;
            PlayerController.mov_y = 0;
        }
    }
}
