using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//https://forum.unity.com/threads/long-press-gesture-on-ugui-button.264388/
public class StickerCtrlScript : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{

    private GameObject MemorialSceneCanvas;
    private Image stickerImage;
    private Sprite stickerSprite;

    private float durationThreshold = 1f;
    private float timePressStarted; 
    private bool isPointerDown = false;
    private bool longPressTriggered = false; 


    // Start is called before the first frame update
    void Start()
    {
        stickerSprite = gameObject.GetComponent<Image>().sprite;
        stickerImage = gameObject.GetComponent<Image>();
        MemorialSceneCanvas = GameObject.Find("MemorialSceneCanvas");

        if (stickerSprite != null)
        {
            SetColor(1f);
        }


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
                SelectSticker();
            }
        }
   

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        /*
        if (eventData.clickTime > 1f)
        {
            gameObject.transform.SetParent(MemorialSceneCanvas.transform);
            Destroy(NewpageUIManager.StickerInventory);
        }
        */
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

    private void SelectSticker()
    {
        gameObject.transform.SetParent(MemorialSceneCanvas.transform);
        Destroy(NewpageUIManager.StickerInventory);
    }


    private void SetColor(float alpha)
    {
        Color color = stickerImage.color;
        color.a = alpha;
        stickerImage.color = color;
    }


}
