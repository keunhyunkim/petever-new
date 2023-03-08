using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetlossTest : MonoBehaviour
{
    private const int TESTNUM = 3;
    CanvasGroup testCanvas;
    CanvasGroup chatNnoti;

    void Start()
    {
        testCanvas = GameObject.Find("PetLossTestPannel").GetComponent<CanvasGroup>();
        chatNnoti = GameObject.Find("ChatAndNoti").GetComponent<CanvasGroup>();
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
    
    private void OnTriggerEnter(Collider collision)
    {
        PetlossTestClass.testCnt++;
        if (this.name == "Yes") {
            PetlossTestClass.yesCnt++;
        } else if (this.name == "Unknown") {
            PetlossTestClass.unknownCnt++;
        } else if (this.name == "No") {
            PetlossTestClass.noCnt++;
        }

        if (PetlossTestClass.testCnt == TESTNUM) {
            // All Tests are done.
            GameObject petlossTestObj = GameObject.FindGameObjectWithTag("PetLossTest");
            petlossTestObj.SetActive(false);
            PetlossTestClass.testCnt = 0;

            hidePetLossTest();
        } else {
            collision.transform.position = GameObject.Find("TestPosition").transform.position;
        }

        
    }
}
