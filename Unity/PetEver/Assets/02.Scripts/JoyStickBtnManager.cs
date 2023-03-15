using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickBtnManager : MonoBehaviour
{
    public List<GameObject> Buttons { get; set; } = new List<GameObject>();
    string resourceUrl = "Prefabs/Buttons/";
    
    void Awake()
    {
        Buttons = new List<GameObject>();
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
        addIcon("PostItBtn");
        addIcon("CandleBtn");
        addIcon("FlowerBtn");
    }
    public void showTreeIcons()
    {
        addIcon("CandleBtn");
        addIcon("FlowerBtn");
    }
    public void showPhotoIcons()
    {
        addIcon("AlbumBtn");
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
    void addIcon(string btnName)
    {
        GameObject btnPrefab = Resources.Load<GameObject>(resourceUrl + btnName);
        GameObject btn = Instantiate(btnPrefab, btnPrefab.transform.position, btnPrefab.transform.rotation) as GameObject;
        
        GameObject joyStickBtns = GameObject.Find("JoystickBtns");
        btn.transform.SetParent(joyStickBtns.transform, false);
        
        if (Buttons.Count > 0)
        {
            setButtonPosition(btn);
        }
        if (Buttons.Contains(btn) == false)
        {
            Buttons.Add(btn);
        }
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
            newBtnPos.y = newBtnPos.y - (widthBetween*2);
            btn.transform.position = newBtnPos;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Wall")
        {
            showWallIcons();
        }
        else if (collision.gameObject.tag == "Frame")
        {
            showPhotoIcons();
        }
        else if (collision.gameObject.name == "MemorialCube")
        {
            showTreeIcons();
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (Buttons.Count > 0)
        {
            deleteIcons();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Wall")
        {
            showWallIcons();
        }
        else if (collision.gameObject.tag == "Frame")
        {
            showPhotoIcons();
        }
        else if (collision.gameObject.name == "MemorialCube")
        {
            showTreeIcons();
        }
    }

    private void OnTriggerExit(Collider collision)
    {

        if (Buttons.Count > 0)
        {
            deleteIcons();
        }

    }
}
