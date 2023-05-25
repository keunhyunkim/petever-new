using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//https://forum.unity.com/threads/long-press-gesture-on-ugui-button.264388/
public class StickerCtrlScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private GameObject MemorialSceneCanvas;
    private Image stickerImage;

    private float durationThreshold = 1f;
    private float timePressStarted;
    private bool isPointerDown = false;
    private bool longPressTriggered = false;


    // Start is called before the first frame update
    void Start()
    {
        stickerImage = gameObject.GetComponent<Image>();
        MemorialSceneCanvas = GameObject.Find("TextAndSticker");

    }

    // Update is called once per frame
    void Update()
    {

        if (isPointerDown && !longPressTriggered)
        {
            if (Time.time - timePressStarted > durationThreshold)
            {
                longPressTriggered = true;
                SelectSticker();
            }
        }


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
        var StickerInventory = GameObject.Find("StickerInventory(Clone)");
        Destroy(StickerInventory);
    }

}
