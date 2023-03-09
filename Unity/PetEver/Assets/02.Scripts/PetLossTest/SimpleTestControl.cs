using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTestControl : MonoBehaviour
{
    CanvasGroup testCanvas;
    CanvasGroup testGuide;
    CanvasGroup chatNnoti;
    public GameObject simpleCG;

    void Start()
    {
        testCanvas = GameObject.Find("PetLossTestPannel").GetComponent<CanvasGroup>();
        testGuide = GameObject.Find("PetLossTestGuide").GetComponent<CanvasGroup>();
        chatNnoti = GameObject.Find("ChatAndNoti").GetComponent<CanvasGroup>();
    }

    // IEnumerator OneSecDelay()
    // {
    //     yield return new WaitForSeconds(3.0f);
    // }

    void showTestGuide()
    {
        testGuide.alpha = 1;
        testGuide.interactable = true;
        testGuide.blocksRaycasts = true;
    }

    public void OnSimpleSubmitExitClicked()
    {
        // Debug.Log(SimpleTest.totalScore);

        // Initialize the totalScore
        SimpleTest.totalScore = SimpleTest.DEFAULT_TOTALSCORE;
        
        simpleCG.GetComponent<Canvas>().GetComponent<CanvasGroup>().alpha = 0;
        simpleCG.GetComponent<Canvas>().GetComponent<CanvasGroup>().interactable = false;
        simpleCG.GetComponent<Canvas>().GetComponent<CanvasGroup>().blocksRaycasts = false;

        HealingGuide.OnSimpleTestExitClicked();
        showTestGuide();
        // StartCoroutine(OneSecDelay());
    }

    // private void startPetlossTest()
    // {
    //     // StartPoint = GameObject.Find("TestPosition").transform.position;
    //     // collision.transform.position = StartPoint;

    //     showPetLossTest();
    // }
}
