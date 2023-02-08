using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class TextCtrlScript : MonoBehaviour, IPointerClickHandler
{

    private GameObject UserInputTextBGD;
    private GameObject UserInputText;


    // Start is called before the first frame update
    void Start()
    {
        UserInputTextBGD = GameObject.Find("UserInputTextBGD");
        UserInputText = GameObject.Find("UserInputText");

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            Destroy(UserInputTextBGD);
            gameObject.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
