using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoyStickBtnManager : MonoBehaviour
{
    public List<GameObject> Buttons { get; set; } = new List<GameObject>();
    string resourceUrl = "Prefabs/Buttons/";
    GameObject joyStickBtns;

    CanvasGroup stickyNoteInputPanel;
    CanvasGroup photoPanel;

    void Awake()
    {
        Buttons = new List<GameObject>();
        joyStickBtns = GameObject.Find("JoystickBtns");
        stickyNoteInputPanel = GameObject.Find("StickyNotePanel").GetComponent<CanvasGroup>();
        photoPanel = GameObject.Find("PhotoPanel").GetComponent<CanvasGroup>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void showWallIcons()
    {
        GameObject postItBtn = addIcon("PostItBtn");
        postItBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnStickyNoteInputOpenClicked();
        });
        GameObject candleBtn = addIcon("CandleBtn");
        GameObject flowerBtn = addIcon("FlowerBtn");
    }
    public void showTreeIcons()
    {
        addIcon("CandleBtn");
        addIcon("FlowerBtn");
    }
    public void showPhotoIcons()
    {
        GameObject albumBtn = addIcon("AlbumBtn");
        albumBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnShowPhotoClicked();
        });
        addIcon("CandleBtn");
        addIcon("FlowerBtn");
    }

    void deleteIcons()
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
    GameObject addIcon(string btnName)
    {
        GameObject btnPrefab = Resources.Load<GameObject>(resourceUrl + btnName);
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


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Owner")
        {
            if (this.gameObject.name == "Wall")
            {
                showWallIcons();
            }
            else if (this.gameObject.tag == "Frame")
            {
                showPhotoIcons();
            }
            else if (this.gameObject.name == "MemorialCube")
            {
                showTreeIcons();
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Owner")
        {
            if (Buttons.Count > 0)
            {
                deleteIcons();
            }
        }
    }
    public void OnStickyNoteInputOpenClicked()
    {
        showCanvasGroup(stickyNoteInputPanel);
    }
    public void OnShowPhotoClicked()
    {

        GameObject frameGo = GetChildWithName(this.gameObject, "Plane/FrameImageCanvas/PictureImage");
        Image uiImage = frameGo.GetComponent<Image>();

        GameObject panelGo = GetChildWithName(photoPanel.gameObject, "Background/Mask Image/MyImage");
        if (panelGo != null)
        {
            Image panelUiImage = panelGo.GetComponent<Image>();
            panelUiImage.sprite = uiImage.sprite;
        }
        else
        {
            Debug.Log("panelGo null");
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
            Debug.Log("CanvasGroup null in showCanvasGroup");
        }

    }

    GameObject GetChildWithName(GameObject obj, string childName)
    {
        return obj.transform.Find(childName)?.gameObject;
    }
}
