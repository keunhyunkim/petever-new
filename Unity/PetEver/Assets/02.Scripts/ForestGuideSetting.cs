using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForestGuideSetting : MonoBehaviour
{

    GameObject forestGuide;
    GameObject navi1;
    GameObject navi2;
    GameObject navi3;
    GameObject nextBtn1;
    GameObject nextBtn2;
    GameObject skipBtn;
    GameObject doneBtn;
    GameObject chatYellow;
    GameObject chatBlack;
    GameObject guide1;
    GameObject guide2;
    GameObject guide3;
    public static bool isGuided = false;

    GameObject manCharacter;
    GameObject mainEvent;
    GameObject mainCanvas;

    GameObject forestCanvas;

    public GameObject canvasPrefab;
    public GameObject eventSystemPrefab;

    GameObject previousCanvas;
    GameObject previouEventSystem;

    void Awake() {
        manCharacter = GameObject.FindGameObjectWithTag("Owner");
        mainEvent = GameObject.FindGameObjectWithTag("MainEventSystem");
        mainCanvas = GameObject.FindGameObjectWithTag("UICanvas");
        if (mainCanvas) {
            mainCanvas.SetActive(false);
        }

        forestGuide = GameObject.FindGameObjectWithTag("ForestGuide");
        if (isGuided == true) {
            forestGuide.SetActive(false);
            mainCanvas.SetActive(true);
            startWalking();
        } else {
            isGuided = true;

            chatYellow = GameObject.Find("ForestGuideChat");
            chatBlack = GameObject.Find("ForestGuideChatBlack");

            guide1 = GameObject.Find("ForestGuide1");
            guide2 = GameObject.Find("ForestGuide2");
            guide3 = GameObject.Find("ForestGuide3");

            navi1 = GameObject.Find("Navigator_oxx");
            navi2 = GameObject.Find("Navigator_xox");
            navi3 = GameObject.Find("Navigator_xxo");
            nextBtn1 = GameObject.Find("Navigator_right");
            nextBtn2 = GameObject.Find("Navigator_right2");

            skipBtn = GameObject.Find("ForestGuideSkip");
            doneBtn = GameObject.Find("ForestGuideDone");

            doneBtn.SetActive(false);

            chatBlack.SetActive(false);

            navi1.SetActive(true);
            navi2.SetActive(false);
            navi3.SetActive(false);
            nextBtn1.SetActive(true);
            nextBtn2.SetActive(false);
        }
    }

    void startWalking()
    {
        previousCanvas = GameObject.FindGameObjectWithTag("ForestCanvas");
        previouEventSystem = GameObject.FindGameObjectWithTag("MainEventSystem");
        if (previousCanvas == null)
        {
            GameObject canvas = Instantiate(canvasPrefab) as GameObject;
        } else {
        }
        if (previouEventSystem == null)
        {
            GameObject eSystem = Instantiate(eventSystemPrefab) as GameObject;
        }
    }

    public void onForestGuideExit()
    {
        forestGuide.SetActive(false);
        mainCanvas.SetActive(true);
        startWalking();
    }

    public void onNextFrom1st() {
        chatYellow.SetActive(false);
        navi1.SetActive(false);

        chatBlack.SetActive(true);
        navi2.SetActive(true);
        nextBtn2.SetActive(true);

        guide3.SetActive(false);
    }

    public void onNextFrom2nd() {
        navi2.SetActive(false);
        guide2.SetActive(false);

        navi3.SetActive(true);
        guide3.SetActive(true);

        skipBtn.SetActive(false);
        doneBtn.SetActive(true);
    }
}
