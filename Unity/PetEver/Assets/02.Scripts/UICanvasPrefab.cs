using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UICanvasPrefab : MonoBehaviour
{

    public GameObject canvasPrefab;
    public GameObject eventSystemPrefab;

    GameObject previousCanvas;
    GameObject previouEventSystem;

    void Awake()
    {
        previousCanvas = GameObject.FindGameObjectWithTag("UICanvas");
        previouEventSystem = GameObject.FindGameObjectWithTag("MainEventSystem");
        if (previousCanvas == null)
        {
            GameObject canvas = Instantiate(canvasPrefab) as GameObject;
        }
        if (previouEventSystem == null)
        {
            GameObject eSystem = Instantiate(eventSystemPrefab) as GameObject;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
