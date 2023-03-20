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

    private float durationThreshold = 1f;
    private float timePressStarted; 
    private bool isPointerDown = false;
    private bool longPressTriggered = false; 

    // Start is called before the first frame update


    void Start()
    {
        UserInputTextBGD = GameObject.Find("UserInputTextBGD");
        UserInputText = GameObject.Find("UserInputText");
        inputField = gameObject.GetComponent<TMP_InputField>();

        inputField.onSubmit.AddListener(delegate{ LockInput(); });


    }

    // Update is called once per frame
    void Update()
    {
        if(isPointerDown && !longPressTriggered)
        {
            if(Time.time - timePressStarted > durationThreshold)
            {
                longPressTriggered = true;
                CompleteWriting();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        timePressStarted = Time.time;
        isPointerDown = true;
        longPressTriggered = false; 
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
    }

    private void CompleteWriting()
    {

        Destroy(UserInputTextBGD);
        gameObject.SetActive(true);
        LockInput();
    }


    private void LockInput()
    {
        if (inputField.readOnly == true)
            inputField.readOnly = false;
        else if (inputField.readOnly == false)
            inputField.readOnly = true;        
    }
}



