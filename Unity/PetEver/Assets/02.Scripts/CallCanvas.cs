using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CallCanvas : MonoBehaviour
{
    GameObject ManCharacter;
    GameObject mainCanvas;
    GameObject mainEventSystem;
    GameObject CanvasEventSystem;
    GameObject DrawCanvas;
    GameObject DrawCanvasCamera;
    GameObject MainCamera;

    private bool checkDraw;

    void Start()
    {
        checkDraw = false;

        ManCharacter = GameObject.Find("Man");
        mainEventSystem = GameObject.Find("MainEventSystem");
        mainCanvas = GameObject.Find("MainCanvas");
        CanvasEventSystem = GameObject.Find("DrawCanvasEvent");
        DrawCanvas = GameObject.Find("DrawCanvas");
        DrawCanvasCamera = GameObject.Find("DrawCanvasCamera");
        MainCamera = GameObject.Find("MainCamera");

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("newScene"));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (checkDraw == false) {
            ManCharacter.SetActive(false);
            mainEventSystem.SetActive(false);
            mainCanvas.SetActive(false);
            MainCamera.GetComponent<Camera>().enabled = false;

            DrawingSettings.SetEraseAll();
            CanvasEventSystem.SetActive(true);
            DrawCanvas.SetActive(true);
            DrawCanvasCamera.GetComponent<Camera>().enabled = true;

            checkDraw = true;    
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        checkDraw = false;
    }
}
