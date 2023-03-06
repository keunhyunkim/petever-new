using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class TextCtrlScript : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{

    private GameObject UserInputTextBGD;
    private GameObject UserInputText;
    private InputField inputField;

    private float durationThreshold = 1f;
    private float timePressStarted; 
    private bool isPointerDown = false;
    private bool longPressTriggered = false; 

    // Start is called before the first frame update
    void Start()
    {
        UserInputTextBGD = GameObject.Find("UserInputTextBGD");
        UserInputText = GameObject.Find("UserInputText");
        inputField = GameObject.Find("UserInputText").GetComponent<InputField>();

    }

    // Update is called once per frame
    void Update()
    {
        if(isPointerDown && !longPressTriggered)
        {
            if(Time.time - timePressStarted > durationThreshold)
            {
                Debug.Log("press");
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
        inputField.readOnly = true;

    }
}

