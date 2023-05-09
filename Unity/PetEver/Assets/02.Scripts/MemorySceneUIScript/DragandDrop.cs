using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// pinch to scale an object : https://forum.unity.com/threads/pinch-in-order-to-scale-an-object.808692/



public class DragandDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    // Start is called before the first frame update
    void Start()
    {

    }



    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 currentPos = eventData.position;
        this.transform.position = currentPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (gameObject.name == "UserInputText")
        {
            gameObject.GetComponent<TextCtrlScript>().LockInput();
        }
        //Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //this.transform.position = touchPos;

    }

}
