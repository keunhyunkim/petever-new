using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UICanvasPrefab : MonoBehaviour
{

    public GameObject canvasPrefab;

    public GameObject previousCanvas;

    void Awake()
    {
        Debug.Log("Awwake()");
        previousCanvas = GameObject.FindGameObjectWithTag("UICanvas");
        if (previousCanvas == null)
        {

            Debug.Log("previousCanvas null");
            try
            {
                GameObject canvas = Instantiate(canvasPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

        }
        else
        {
            Debug.Log("UI Canvas exist");
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
