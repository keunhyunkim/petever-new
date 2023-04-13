using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FurModifyUIControl : MonoBehaviour
{

    public Button longBtn, middleBtn, shortBtn;

    public Button feetup_both, feetup_left, feetup_right, lie_down, sitting, run, snuffing, scratching, walking, tailing, turning, TBD;

    public Slider detailSlider;
    public GameObject bodyfur_back, bodyfur_middle, bodyfur_front;
    public GameObject chinfur;
    public GameObject neckfur; 
    private Vector3 longFurValue_body, middleFurValue_body, shortFurValue_body, presentFurValue_body;
    private Vector3 longFurValue_neck, middleFurValue_neck, shortFurValue_neck, presentFurValue_neck;
    private Vector3 longFurValue_chin, middleFurValue_chin, shortFurValue_chin, presentFurValue_chin;


    private int animFlag = 0;
    private Animator dogAnimator;



    void Awake()
    {
        longBtn = GameObject.Find("LongBtn").GetComponent<Button>();
        middleBtn = GameObject.Find("MiddleBtn").GetComponent<Button>();
        shortBtn = GameObject.Find("ShortBtn").GetComponent<Button>();

        feetup_both = GameObject.Find("feetup_both").GetComponent<Button>();
        feetup_left = GameObject.Find("feetup_left").GetComponent<Button>();
        feetup_right = GameObject.Find("feetup_right").GetComponent<Button>();
        lie_down = GameObject.Find("lie_down").GetComponent<Button>();
        sitting = GameObject.Find("sitting").GetComponent<Button>();
        run = GameObject.Find("run").GetComponent<Button>();
        snuffing = GameObject.Find("snuffing").GetComponent<Button>();
        scratching = GameObject.Find("scratching").GetComponent<Button>();
        walking = GameObject.Find("walking").GetComponent<Button>();
        tailing = GameObject.Find("tailing").GetComponent<Button>();
        turning = GameObject.Find("turning").GetComponent<Button>();
        TBD = GameObject.Find("TBD").GetComponent<Button>();



        detailSlider = GameObject.Find("DetailSlider").GetComponent<Slider>();

        bodyfur_back = GameObject.Find("bodyfur_back");
        bodyfur_middle = GameObject.Find("bodyfur_middle");
        bodyfur_front = GameObject.Find("bodyfur_front");
        neckfur = GameObject.Find("neckfur");
        chinfur = GameObject.Find("chinfur");


        longFurValue_body = new Vector3(1.2f, 1.2f, 1.2f );
        middleFurValue_body = new Vector3(0.7f, 0.7f, 0.7f );
        shortFurValue_body = new Vector3(0.4f, 0.4f, 0.4f );

        longFurValue_neck = new Vector3(2.0f, 2.0f, 2.0f );
        middleFurValue_neck = new Vector3(1.25f, 1.25f, 1.25f );
        shortFurValue_neck = new Vector3(0.5f, 0.5f, 0.5f );

        longFurValue_chin = new Vector3(1.6f, 1.6f, 1.6f );
        middleFurValue_chin = new Vector3(0.9f, 0.9f, 0.9f );
        shortFurValue_chin = new Vector3(0.2f, 0.2f, 0.2f );

 



        detailSlider.value = 1.0f;

        dogAnimator = GameObject.Find("Pomeranian_long_modifyfur_splitmesh").GetComponent<Animator>();


    }



    // Start is called before the first frame update
    void Start()
    {
        dogAnimator.SetInteger("animFlag", animFlag);

        longBtn.onClick.AddListener(delegate { RoughControl(); });
        middleBtn.onClick.AddListener(delegate { RoughControl(); });
        shortBtn.onClick.AddListener(delegate { RoughControl(); });

        feetup_both.onClick.AddListener(delegate { animTest(); });
        feetup_left.onClick.AddListener(delegate { animTest(); });
        feetup_right.onClick.AddListener(delegate { animTest(); });
        lie_down.onClick.AddListener(delegate { animTest(); });
        sitting.onClick.AddListener(delegate { animTest(); });
        run.onClick.AddListener(delegate { animTest(); });
        snuffing.onClick.AddListener(delegate { animTest(); });
        scratching.onClick.AddListener(delegate { animTest(); });
        walking.onClick.AddListener(delegate { animTest(); });
        tailing.onClick.AddListener(delegate { animTest(); });
        turning.onClick.AddListener(delegate { animTest(); });
        TBD.onClick.AddListener(delegate { animTest(); });


        detailSlider.onValueChanged.AddListener(delegate { DetailControl(); });




    }

    // Update is called once per frame
    void Update()
    {

        

    }


    public void animTest()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "feetup_both"){
            animFlag = 1;
        }
        else if(EventSystem.current.currentSelectedGameObject.name == "feetup_left"){
            animFlag = 2;
        }
        else if(EventSystem.current.currentSelectedGameObject.name == "feetup_right"){
            animFlag = 3;
        }        
        else if(EventSystem.current.currentSelectedGameObject.name == "lie_down"){
            animFlag = 4;
        }
        else if(EventSystem.current.currentSelectedGameObject.name == "run"){
            animFlag = 5;
        }
        else if(EventSystem.current.currentSelectedGameObject.name == "walking"){
            animFlag = 6;
        }
        else if(EventSystem.current.currentSelectedGameObject.name == "scratching"){
            animFlag = 7;
        }
        else if(EventSystem.current.currentSelectedGameObject.name == "sitting"){
            animFlag = 8;
        }
        else if(EventSystem.current.currentSelectedGameObject.name == "snuffing"){
            animFlag = 9;
        }
        else if(EventSystem.current.currentSelectedGameObject.name == "tailing"){
            animFlag = 10;
        }
        else if(EventSystem.current.currentSelectedGameObject.name == "turning"){
            animFlag = 11;
        }
        else if(EventSystem.current.currentSelectedGameObject.name == "TBD"){
            animFlag = 0;
        }

        dogAnimator.SetInteger("animFlag", animFlag);
    }

    public void RoughControl()
    {
        detailSlider.value = 1.0f;
        if (EventSystem.current.currentSelectedGameObject.name == "LongBtn")
        {
            bodyfur_back.transform.localScale = longFurValue_body;
            bodyfur_middle.transform.localScale = longFurValue_body;
            bodyfur_front.transform.localScale = longFurValue_body;

            chinfur.transform.localScale = longFurValue_chin;
            neckfur.transform.localScale = longFurValue_neck;

            presentFurValue_body = longFurValue_body;
            presentFurValue_neck = longFurValue_neck;
            presentFurValue_chin = longFurValue_chin;

        }

        else if (EventSystem.current.currentSelectedGameObject.name == "MiddleBtn")
        {
            bodyfur_back.transform.localScale = middleFurValue_body;
            bodyfur_middle.transform.localScale = middleFurValue_body;
            bodyfur_front.transform.localScale = middleFurValue_body;

            chinfur.transform.localScale = middleFurValue_chin;
            neckfur.transform.localScale = middleFurValue_neck;           

            presentFurValue_body = middleFurValue_body;
            presentFurValue_neck = middleFurValue_neck;
            presentFurValue_chin = middleFurValue_chin;
        }

        else if (EventSystem.current.currentSelectedGameObject.name == "ShortBtn")
        {
            bodyfur_back.transform.localScale = shortFurValue_body;
            bodyfur_middle.transform.localScale = shortFurValue_body;
            bodyfur_front.transform.localScale = shortFurValue_body;

            chinfur.transform.localScale = shortFurValue_chin;
            neckfur.transform.localScale = shortFurValue_neck;

            presentFurValue_body = shortFurValue_body;
            presentFurValue_neck = shortFurValue_neck;
            presentFurValue_chin = shortFurValue_chin;
        }

    }

    public void DetailControl()
    {
        bodyfur_back.transform.localScale = presentFurValue_body*detailSlider.value;
        bodyfur_middle.transform.localScale = presentFurValue_body*detailSlider.value;
        bodyfur_front.transform.localScale = presentFurValue_body*detailSlider.value;

        chinfur.transform.localScale = presentFurValue_chin*detailSlider.value;
        neckfur.transform.localScale = presentFurValue_neck*detailSlider.value;

    }

}
