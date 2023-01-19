using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ForestCanvasPrefab : MonoBehaviour
{

    public GameObject canvasPrefab;
    public GameObject eventSystemPrefab;

    GameObject previousCanvas;
    GameObject previouEventSystem;

    void Awake()
    {
        previousCanvas = GameObject.FindGameObjectWithTag("ForestCanvas");
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
}
