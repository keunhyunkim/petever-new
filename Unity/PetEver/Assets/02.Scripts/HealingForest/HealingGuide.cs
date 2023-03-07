using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealingGuide : MonoBehaviour
{

    GameObject healingGuide;
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
    public static bool isHealingGuided = false;

    GameObject manCharacter;
    GameObject mainEvent;
    private static GameObject mainCanvas;

    public GameObject eventSystemPrefab;

    GameObject previousCanvas;
    GameObject previouEventSystem;

    public GameObject simpleTestPrefab;
    GameObject simpleTestCanvas;


    void Awake() {
        PlayerInput.InitJoystick();
        PlayerController.isForest = false;

        manCharacter = GameObject.FindGameObjectWithTag("Owner");
        mainEvent = GameObject.FindGameObjectWithTag("MainEventSystem");
        mainCanvas = GameObject.FindGameObjectWithTag("UICanvas");
        if (mainCanvas) {
            mainCanvas.SetActive(false);
        }

        healingGuide = GameObject.FindGameObjectWithTag("HealingForestGuide");
        if (isHealingGuided == true) {
            //FIX ME : Allocate when first enter
            Destroy(healingGuide);
            mainCanvas.SetActive(true);
        } else {
            isHealingGuided = true;

            chatYellow = GameObject.Find("HealingGuideChat");
            chatBlack = GameObject.Find("HealingGuideChatBlack");

            guide1 = GameObject.Find("HealingGuide1");
            guide2 = GameObject.Find("HealingGuide2");
            guide3 = GameObject.Find("HealingGuide3");

            navi1 = GameObject.Find("Navigator_oxx");
            navi2 = GameObject.Find("Navigator_xox");
            navi3 = GameObject.Find("Navigator_xxo");
            nextBtn1 = GameObject.Find("Navigator_right");
            nextBtn2 = GameObject.Find("Navigator_right2");

            skipBtn = GameObject.Find("HealingGuideSkip");
            doneBtn = GameObject.Find("HealingGuideDone");

            doneBtn.SetActive(false);

            chatBlack.SetActive(false);

            navi1.SetActive(true);
            navi2.SetActive(false);
            navi3.SetActive(false);
            nextBtn1.SetActive(true);
            nextBtn2.SetActive(false);
        }
    }

    void showSimpleTest()
    {
        simpleTestCanvas = GameObject.FindGameObjectWithTag("SimpleTest");
        if (simpleTestCanvas == null)
        {
            GameObject canvas = Instantiate(simpleTestPrefab) as GameObject;
        }
    }

    public static void OnSimpleTestExitClicked()
    {
        mainCanvas.SetActive(true);
    }

    public void onhealingGuideExit()
    {
        Destroy(healingGuide);
        showSimpleTest();
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
