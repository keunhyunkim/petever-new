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
    public GameObject createTreeButtonPrefab;
    public GameObject treePrefab;

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

        createTreeBtn();
    }


    void Update()
    {

    }

    void createTreeBtn()
    {
        GameObject treeBtn = Instantiate(createTreeButtonPrefab);
        treeBtn.transform.SetParent(mainCanvas.transform);
        treeBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            createTreeInfrontOfCharacter();
        }
        );
    }

    void createTreeInfrontOfCharacter()
    {
        Vector3 pos = manCharacter.transform.position - (manCharacter.transform.forward * 5);
        pos.y = 0;
        GameObject newTree = Instantiate(treePrefab);
        newTree.transform.SetParent(GameObject.Find("GameObject").transform);
        newTree.transform.position = pos;
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
