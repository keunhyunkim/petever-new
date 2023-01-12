using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasControl : MonoBehaviour
{
    GameObject ManCharacter;
    GameObject mainCanvas;
    GameObject mainEventSystem;
    GameObject CanvasEventSystem;
    GameObject DrawCanvas;
    GameObject DrawCanvasCamera;
    GameObject MainCamera;

    void Start()
    {
        ManCharacter = GameObject.FindGameObjectWithTag("Owner");
        mainEventSystem = GameObject.FindGameObjectWithTag("MainEventSystem");
        mainCanvas = GameObject.FindGameObjectWithTag("UICanvas");
        CanvasEventSystem = GameObject.Find("DrawCanvasEvent");
        DrawCanvas = GameObject.Find("DrawCanvas");
        DrawCanvasCamera = GameObject.Find("DrawCanvasCamera");
        MainCamera = GameObject.Find("MainCamera");

        CanvasEventSystem.SetActive(false);
        DrawCanvas.SetActive(false);
        DrawCanvasCamera.GetComponent<Camera>().enabled = false;
    }

    void Update()
    {
        
    }

    public void MoveBackToHome()
    {
        CanvasEventSystem.SetActive(false);
        DrawCanvas.SetActive(false);
        DrawCanvasCamera.GetComponent<Camera>().enabled = false;

        ManCharacter.SetActive(true);
        mainEventSystem.SetActive(true);
        mainCanvas.SetActive(true);
        MainCamera.GetComponent<Camera>().enabled = true;

        PlayerInput.InitJoystick();
    }
}
