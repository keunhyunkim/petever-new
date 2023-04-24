using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MySpaceCanvasScript : MonoBehaviour
{

    public CanvasGroup questPopupPanel;
    public GameObject questSelectionLight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClickQuestBtn(){
        questSelectionLight.SetActive(true);
        showCanvasGroup(questPopupPanel);
    }
    public void onClickCloseBtn(){
        hideCanvasGroup(questPopupPanel);
        questSelectionLight.SetActive(false);
    }

    void showCanvasGroup(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    void hideCanvasGroup(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
}
