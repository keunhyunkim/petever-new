using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoyStickBtnManager : MonoBehaviour
{
    public List<GameObject> Buttons { get; set; } = new List<GameObject>();
    GameObject owner;
    GameObject wallArea;
    GameObject joyStickBtns;

    CanvasGroup stickyNoteInputPanel;
    CanvasGroup photoPanel;
    [SerializeField] private GameObject candlePrefab, flowerPrefab;
    [SerializeField] private GameObject albumBtnPrefab, candleBtnPrefab, flowerBtnPrefab, postItBtnPrefab;

    void Awake()
    {
        Buttons = new List<GameObject>();
        joyStickBtns = GameObject.Find("JoystickBtns");
        stickyNoteInputPanel = GameObject.Find("StickyNotePanel").GetComponent<CanvasGroup>();
        photoPanel = GameObject.Find("PhotoPanel").GetComponent<CanvasGroup>();
        wallArea = GameObject.Find("Wall Area");
        owner = GameObject.FindGameObjectWithTag("Owner");
    }

    void candleAndFlowerBtns()
    {
        GameObject candleBtn = addIcon(candleBtnPrefab);
        candleBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnCandleBtnClicked();
        });
        GameObject flowerBtn = addIcon(flowerBtnPrefab);
        flowerBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnFlowerBtnClicked();
        });
    }


    public void showWallIcons()
    {
        GameObject postItBtn = addIcon(postItBtnPrefab);
        postItBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnStickyNoteInputOpenClicked();
        });
        candleAndFlowerBtns();
    }
    public void showTreeIcons()
    {
        candleAndFlowerBtns();
    }
    public void showPhotoIcons()
    {
        GameObject albumBtn = addIcon(albumBtnPrefab);
        albumBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnShowPhotoClicked();
        });
        candleAndFlowerBtns();
    }

    public void deleteIcons()
    {
        if (Buttons.Count == 0)
        {
            return;
        }
        foreach (GameObject btn in Buttons)
        {
            btn.SetActive(false);
            Destroy(btn);
        }
        Buttons.RemoveAll(item => item == null);
        Buttons.Clear();
    }
    GameObject addIcon(GameObject btnPrefab)
    {
        
        GameObject btn = Instantiate(btnPrefab, btnPrefab.transform.position, btnPrefab.transform.rotation) as GameObject;


        btn.transform.SetParent(joyStickBtns.transform, false);

        if (Buttons.Count > 0)
        {
            setButtonPosition(btn);
        }
        if (Buttons.Contains(btn) == false)
        {
            Buttons.Add(btn);
        }
        return btn;
    }

    int widthBetween = 60;
    void setButtonPosition(GameObject btn)
    {
        if (Buttons.Count == 1)
        {
            Vector3 pos = Buttons[0].transform.position;
            pos.y = pos.y + widthBetween;
            Buttons[0].transform.position = pos;

            Vector3 newBtnPos = btn.transform.position;
            newBtnPos.y = newBtnPos.y - widthBetween;
            btn.transform.position = newBtnPos;
        }
        else if (Buttons.Count == 2)
        {
            Vector3 pos = Buttons[0].transform.position;
            pos.y = pos.y + widthBetween;
            Buttons[0].transform.position = pos;

            Vector3 pos1 = Buttons[1].transform.position;
            pos1.y = pos1.y + widthBetween;
            Buttons[1].transform.position = pos1;

            Vector3 newBtnPos = btn.transform.position;
            newBtnPos.y = newBtnPos.y - (widthBetween * 2);
            btn.transform.position = newBtnPos;
        }
    }



    public void OnStickyNoteInputOpenClicked()
    {
        showCanvasGroup(stickyNoteInputPanel);
    }


    public void OnCandleBtnClicked()
    {
        
        Vector3 pos =  owner.transform.localPosition + (owner.transform.forward * 3);
        GameObject candle = Instantiate(candlePrefab, pos, candlePrefab.transform.rotation) as GameObject;

        Transform candleParent = TransformExtension.FindChildByRecursive(wallArea.transform, "Candles");
        if (candleParent != null)
        {
            candle.transform.SetParent(candleParent, false);
        }
    }
    public void OnFlowerBtnClicked()
    {
        
       
        Vector3 pos =  owner.transform.localPosition + (owner.transform.forward * 3);
        GameObject flower = Instantiate(flowerPrefab, pos, flowerPrefab.transform.rotation) as GameObject;

        Transform flowerParent = TransformExtension.FindChildByRecursive(wallArea.transform, "Flowers");

        if (flowerParent != null)
        {
            flower.transform.SetParent(flowerParent, false);
        }

    }
    public void OnShowPhotoClicked()
    {

        GameObject frameGo = GetChildWithName(this.gameObject, "Plane/FrameImageCanvas/Mask Image/Image");
        Image uiImage = frameGo.GetComponent<Image>();

        GameObject panelGo = GetChildWithName(photoPanel.gameObject, "Background/Mask Image/MyImage");
        if (panelGo != null)
        {
            Image panelUiImage = panelGo.GetComponent<Image>();
            panelUiImage.sprite = uiImage.sprite;
        }
        else
        {

        }

        showCanvasGroup(photoPanel);
    }

    void showCanvasGroup(CanvasGroup cg)
    {
        if (cg != null)
        {
            cg.alpha = 1;
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }
        else
        {

        }

    }

    GameObject GetChildWithName(GameObject obj, string childName)
    {
        return obj.transform.Find(childName)?.gameObject;
    }
}
