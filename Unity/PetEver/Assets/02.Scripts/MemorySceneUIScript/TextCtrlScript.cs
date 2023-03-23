using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class TextCtrlScript : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{

    private GameObject UserInputTextBGD;
    private GameObject UserInputText;
    private TMP_InputField inputField;

    private float durationThreshold = 0.2f;
    private float timePressed; 
    private bool isPointerDown = false;
    //private bool longPressTriggered = false; 

    // Start is called before the first frame update


    void Start()
    {
        UserInputTextBGD = GameObject.Find("UserInputTextBGD");
        UserInputText = GameObject.Find("UserInputText");
        inputField = gameObject.GetComponent<TMP_InputField>();

        inputField.onEndEdit.AddListener(delegate{ CompleteWriting(); });


    }

    // Update is called once per frame
    void Update()
    {

       // Debug.Log(inputField.readOnly);
        if(isPointerDown)
        {
            timePressed = Time.deltaTime;                
        }
        else
        {
            timePressed = 0;
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;

        if(timePressed <= durationThreshold)
        {
            inputField.readOnly = false;
        }
        else
        {
            inputField.readOnly = true;
        }

    }

    private void CompleteWriting()
    {
        LockInput();
        Destroy(UserInputTextBGD);
        gameObject.SetActive(true);
    }


    public void LockInput()
    {
        inputField.readOnly = true;
    }
}



