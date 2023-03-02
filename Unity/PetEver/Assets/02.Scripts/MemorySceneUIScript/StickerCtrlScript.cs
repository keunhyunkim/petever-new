using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class StickerCtrlScript : MonoBehaviour, IPointerClickHandler
{

    private GameObject MemorialSceneCanvas;
    private Image stickerImage;
    private Sprite stickerSprite;

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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 1)
        {
            gameObject.transform.SetParent(MemorialSceneCanvas.transform);
            Destroy(NewpageUIManager.StickerInventory);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetColor(float alpha)
    {
        Color color = stickerImage.color;
        color.a = alpha;
        stickerImage.color = color;
    }

}
