using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour
{
    GameObject manCharacter;
    GameObject mainCanvas;
    GameObject mainEventSystem;
    GameObject CanvasEventSystem;
    GameObject DrawCanvas;
    GameObject DrawCanvasCamera;
    GameObject MainCamera;
    GameObject readWriteEnabledImageToDrawOn;
    // public GameObject createTreeButtonPrefab;

    void Start()
    {
        manCharacter = GameObject.FindGameObjectWithTag("Owner");
        mainEventSystem = GameObject.FindGameObjectWithTag("MainEventSystem");
        mainCanvas = GameObject.FindGameObjectWithTag("UICanvas");
        CanvasEventSystem = GameObject.Find("DrawCanvasEvent");
        DrawCanvas = GameObject.Find("DrawCanvas");
        DrawCanvasCamera = GameObject.Find("DrawCanvasCamera");
        MainCamera = GameObject.Find("MainCamera");
        readWriteEnabledImageToDrawOn = GameObject.Find("ReadWriteEnabledImageToDrawOn");

        CanvasEventSystem.SetActive(false);
        DrawCanvas.SetActive(false);
        readWriteEnabledImageToDrawOn.SetActive(false);
        DrawCanvasCamera.GetComponent<Camera>().enabled = false;

    }


    void Update()
    {

    }

    public void MoveBackToHome()
    {
        CanvasEventSystem.SetActive(false);
        DrawCanvas.SetActive(false);
        readWriteEnabledImageToDrawOn.SetActive(false);
        DrawCanvasCamera.GetComponent<Camera>().enabled = false;

        manCharacter.SetActive(true);
        mainEventSystem.SetActive(true);
        mainCanvas.SetActive(true);
        MainCamera.GetComponent<Camera>().enabled = true;

        PlayerInput.InitJoystick();
    }
}
