using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class GuideUIControl : MonoBehaviour
{
    private Transform left, middle, right, Tab;
    private Button triangleBtn, skipCompleteBtn;
    private GameObject GuideUI;
    private Vector3 createPoint; 
    private float circlePos;
    public Sprite circle_empty, circle_filled;
    public TextMeshProUGUI buttonText;

    
    void Awake() 
    {
        GuideUI = GameObject.Find("GuideUI");
        createPoint = GameObject.Find("MemorialSceneCanvas").GetComponent<RectTransform>().anchoredPosition;
        
        Tab = GameObject.Find("Tab").GetComponent<Transform>();
       // left = GameObject.Find("left");
        //middle = GameObject.Find("middle");
       // right = GameObject.Find("right");
        triangleBtn = GameObject.Find("triangleBtn").GetComponent<Button>();
        skipCompleteBtn = GameObject.Find("skipCompleteBtn").GetComponent<Button>();
        circle_empty = Resources.Load<Sprite>("circle_empty");
        circle_filled = Resources.Load<Sprite>("circle_filled");
        circlePos = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(GuideUI, createPoint, Quaternion.identity, GameObject.Find("MemorialSceneCanvas").GetComponent<RectTransform>());
        triangleBtn.onClick.AddListener(delegate { turnpage(); });
        skipCompleteBtn.onClick.AddListener(delegate {turnpage();} );
    }


    public void turnpage()
    {
        circlePos += 1f;
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;        

        if (clickObject.name == "skipCompleteBtn")
        {
            if (skipCompleteBtn.GetComponentInChildren<TextMeshProUGUI>().text == "건너뛰기")
            {
                circlePos = 2f;
            }

            else if(skipCompleteBtn.GetComponentInChildren<TextMeshProUGUI>().text == "완료")
            {
                Destroy(GuideUI);
            }
            //buttonText = clickObject.GetComponentInChildren<TextMeshProUGUI>();
        }
           
        switch (circlePos%3)
        {
            case 0:
                   Tab.GetChild(0).GetComponent<Image>().sprite = circle_filled;
                   Tab.GetChild(1).GetComponent<Image>().sprite = circle_empty;
                   Tab.GetChild(2).GetComponent<Image>().sprite = circle_empty;
                   skipCompleteBtn.GetComponentInChildren<TextMeshProUGUI>().text = "건너뛰기";
            break;

            case 1:
                   Tab.GetChild(0).GetComponent<Image>().sprite = circle_empty;
                   Tab.GetChild(1).GetComponent<Image>().sprite = circle_filled;
                   Tab.GetChild(2).GetComponent<Image>().sprite = circle_empty;
            break;          

            case 2:
                   Tab.GetChild(0).GetComponent<Image>().sprite = circle_empty;
                   Tab.GetChild(1).GetComponent<Image>().sprite = circle_empty;
                   Tab.GetChild(2).GetComponent<Image>().sprite = circle_filled;
                   skipCompleteBtn.GetComponentInChildren<TextMeshProUGUI>().text = "완료";
            break;

            default:
            break;


        }
        
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
