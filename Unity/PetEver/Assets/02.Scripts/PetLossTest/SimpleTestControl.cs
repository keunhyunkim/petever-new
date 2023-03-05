using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTestControl : MonoBehaviour
{

    public GameObject canvasPrefab;
    GameObject previousCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Owner")
        {
            previousCanvas = GameObject.FindGameObjectWithTag("SimpleTest");
            if (previousCanvas == null)
            {
                GameObject canvas = Instantiate(canvasPrefab) as GameObject;
            }
        }

    }

    public void OnExitClicked()
    {
        // Debug.Log(SimpleTest.totalScore);
        
        // Initialize the totalScore
        SimpleTest.totalScore = SimpleTest.DEFAULT_TOTALSCORE;
        previousCanvas = GameObject.FindGameObjectWithTag("SimpleTest");
        Destroy(previousCanvas, 0.5f);
    }
}
