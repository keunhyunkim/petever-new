using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// reference : https://tenlie10.tistory.com/115
public class PlayerInput : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Image JoystickBGD;
    private Image JoystickPad;
    private Vector3 inputVector;

    //assign player's input value for property
    public float joystick_x { get; private set; } // 
    public float joystick_y { get; private set; } // 
    public Vector3 lastpos { get; private set; } // save last joystick vector not to initialize character's direction

    void Start()
    {
        JoystickBGD = GetComponent<Image>();
        JoystickPad = transform.GetChild(0).GetComponent<Image>();
        lastpos = Vector3.one*99999; // put invalid value 

    }

    private void Update()
    {
        joystick_x = inputVector.x;
        joystick_y = inputVector.y;

    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(JoystickBGD.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x = (pos.x / JoystickBGD.rectTransform.sizeDelta.x);
            pos.y = (pos.y / JoystickBGD.rectTransform.sizeDelta.y);
            inputVector = new Vector3(pos.x * 2, pos.y * 2, 0);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            // joystick control pad moving by user's drag
            JoystickPad.rectTransform.anchoredPosition = new Vector3(inputVector.x * (JoystickBGD.rectTransform.sizeDelta.x / 3)
                                                                     , inputVector.y * (JoystickBGD.rectTransform.sizeDelta.y / 3));

        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        lastpos = Vector3.one*99999; // put invalid value
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        lastpos = inputVector;
       
        // reset JoystickPad position 
        inputVector = Vector3.zero;
        JoystickPad.rectTransform.anchoredPosition = Vector3.zero;
    }

    public float GetHorizontalValue()
    {
        return inputVector.x;
    }

    public float GetVerticalValue()
    {
        return inputVector.y;
    }
}

