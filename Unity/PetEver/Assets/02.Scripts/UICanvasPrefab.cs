using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UICanvasPrefab : MonoBehaviour
{

    public GameObject canvasPrefab;

    public GameObject previousCanvas;
    public 

    void Awake()
    {
        previousCanvas = GameObject.FindGameObjectWithTag("UICanvas");
        if (previousCanvas == null)
        {

            try
            {
                GameObject canvas = Instantiate(canvasPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
