using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetlossTestStart : MonoBehaviour
{
    Vector3 StartPoint;
    public GameObject petlossTestPrefab;
    CanvasGroup testCanvas;
    CanvasGroup chatNnoti;


    void Start()
    {
        testCanvas = GameObject.Find("PetLossTestPannel").GetComponent<CanvasGroup>();
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

    private void hideGuide() {
        GameObject.Find("PetLossTestGuide").SetActive(false);
    }

    public void startPetlossTest()
    {
        // StartPoint = GameObject.Find("TestPosition").transform.position;
        // collision.transform.position = StartPoint;

        //Hide the Guide after 2 seconds
        Invoke("hideGuide", 2.0f);

        showPetLossTest();
    }

    private void OnTriggerEnter(Collider collision)
    {

    }
}
