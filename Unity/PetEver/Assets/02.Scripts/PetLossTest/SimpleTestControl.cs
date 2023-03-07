using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTestControl : MonoBehaviour
{
    public void OnExitClicked()
    {
        // Debug.Log(SimpleTest.totalScore);
        
        // Initialize the totalScore
        SimpleTest.totalScore = SimpleTest.DEFAULT_TOTALSCORE;
        GameObject previousCanvas = GameObject.FindGameObjectWithTag("SimpleTest");
        Destroy(previousCanvas, 0.5f);
        HealingGuide.OnSimpleTestExitClicked();
    }
}
