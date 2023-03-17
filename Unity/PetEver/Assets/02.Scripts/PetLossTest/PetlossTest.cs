using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PetlossTest : MonoBehaviour
{
    private const int TESTNUM = 1;
    CanvasGroup testCanvas;
    CanvasGroup chatNnoti;
    CanvasGroup testGuide;

    void Start()
    {
        testCanvas = GameObject.Find("PetLossTestPannel").GetComponent<CanvasGroup>();
        chatNnoti = GameObject.Find("ChatAndNoti").GetComponent<CanvasGroup>();
        testGuide = GameObject.Find("PetLossTestGuide").GetComponent<CanvasGroup>();
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

    private void showTestGuide()
    {
        testGuide.alpha = 1;
        testGuide.interactable = true;
        testGuide.blocksRaycasts = true;
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Owner") {
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

                // Move the character to Recommended Course
                collision.transform.position = GameObject.Find("RecommendPos").transform.position;
                
                // Show the Character's petloss status and recommend the course
                
                GameObject testGuideText = GameObject.Find("PetLossGuide1");
                string msg = "마음이 아직 완전히 회복되시지\\n않으신 것 같아요.\\n오늘은 탄이와의 추억을\\n이미지로 꾸며볼까요?";
                msg = msg.Replace("\\n", "\n");
                testGuideText.GetComponent<TextMeshProUGUI>().text = msg;
                showTestGuide();
                PetlossTestStart.isRecommended = true;


            } else {
                collision.transform.position = GameObject.Find("TestPosition").transform.position;
            }

        }
    }
}
