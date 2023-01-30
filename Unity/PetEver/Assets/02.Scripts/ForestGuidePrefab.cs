using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ForestGuidePrefab : MonoBehaviour
{

    public GameObject guidePrefab;
    public GameObject eventSystemPrefab;

    GameObject previousCanvas;
    GameObject previouEventSystem;

    void Awake()
    {
        previousCanvas = GameObject.FindGameObjectWithTag("ForestGuide");
        previouEventSystem = GameObject.FindGameObjectWithTag("MainEventSystem");
        if (previousCanvas == null)
        {
            GameObject canvas = Instantiate(guidePrefab) as GameObject;
        }
        if (previouEventSystem == null)
        {
            GameObject eSystem = Instantiate(eventSystemPrefab) as GameObject;
        }
    }
}
