using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetlossTestStart : MonoBehaviour
{
    Vector3 StartPoint;
    public GameObject petlossTestPrefab;
    CanvasGroup testCanvas;
    CanvasGroup testGuide;
    CanvasGroup chatNnoti;

    public static bool isRecommended = false;
    void Start()
    {
        testCanvas = GameObject.Find("PetLossTestPannel").GetComponent<CanvasGroup>();
        testGuide = GameObject.Find("PetLossTestGuide").GetComponent<CanvasGroup>();
        chatNnoti = GameObject.Find("ChatAndNoti").GetComponent<CanvasGroup>();
    }

    private void showPetLossTest()
    {
        testCanvas.alpha = 1;
        testCanvas.interactable = true;
        testCanvas.blocksRaycasts = true;

        chatNnoti.alpha = 0;
        chatNnoti.interactable = false;
        chatNnoti.blocksRaycasts = false;
    }

    private void hidePetLossTest()
    {
        testCanvas.alpha = 0;
        testCanvas.interactable = false;
        testCanvas.blocksRaycasts = false;

        chatNnoti.alpha = 1;
        chatNnoti.interactable = true;
        chatNnoti.blocksRaycasts = true;
    }

    void hideTestGuide()
    {
        testGuide.alpha = 0;
        testGuide.interactable = false;
        testGuide.blocksRaycasts = false;
    }

    private void hideGuideNstartTest() {
        hideTestGuide();
        showPetLossTest();
    }

    public void startPetlossTest()
    {
        // StartPoint = GameObject.Find("TestPosition").transform.position;
        // collision.transform.position = StartPoint;

        if (isRecommended == false) {
            //When touching the screen, hide the Guide
            hideGuideNstartTest();
        } else {
            hideTestGuide();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {

    }
}
