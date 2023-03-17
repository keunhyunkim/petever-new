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
        GameObject canvas = Instantiate(guidePrefab) as GameObject;
    }
}
