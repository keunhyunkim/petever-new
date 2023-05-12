using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class SetCharacterFeature : MonoBehaviour
{
    private Vector3 dogSummonPos = new Vector3(-550f, 1070f, -10f);
    private Vector3 dogScale = new Vector3(60f, 60f, 60f);

    public GameObject maltesePrefab;
    public GameObject pomeLongPrefab;
    public GameObject pomeShortPrefab;
    public GameObject pugPrefab;
    public GameObject shihtzuPrefab;
    public GameObject retrieverPrefab;
    public GameObject poodlePrefab;

    //declare variables for control fur length 
    public GameObject bodyfur_back, bodyfur_middle, bodyfur_front;
    public GameObject chinfur;
    public GameObject neckfur; 
    private Vector3 longFurValue_body, middleFurValue_body, shortFurValue_body, presentFurValue_body;
    private Vector3 longFurValue_neck, middleFurValue_neck, shortFurValue_neck, presentFurValue_neck;
    private Vector3 longFurValue_chin, middleFurValue_chin, shortFurValue_chin, presentFurValue_chin;

    private Material dogMaterial;
    
    public Material pomeLong_black;
    public Material pomeLong_brown;
    public Material pomeLong_white;

    public Material pomeShort_black;
    public Material pomeShort_brown;
    public Material pomeShort_white;

    public Material pug_black;
    public Material pug_brown;

    public Material maltese_white;

    private SkinnedMeshRenderer meshRenderer; 

    private String breed = "";
    private String petName = "";
    private String section1Color = "";
    private String section2Color = "";
    private AndroidJavaObject activityContext = null;
    [SerializeField] private GameObject CharacterParent;

    void Awake()
    {
        //Vector3 dogScale = new Vector3(2f, 2f, 2f);

        using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            breed = "";
            petName = "";
            GameObject dogPrefab;
            try
            {
                activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");

                AndroidJavaObject intent = activityContext.Call<AndroidJavaObject>("getIntent");
                breed = intent.Call<String>("getStringExtra", "breed");
                petName = intent.Call<String>("getStringExtra", "petname");
                section1Color = intent.Call<String>("getStringExtra", "section1Color");
                section2Color = intent.Call<String>("getStringExtra", "section2Color");

                Debug.Log("[intent data] arguments : " + breed);
                Debug.Log("[intent data] arguments : " + petName);
                Debug.Log("[intent data] arguments : " + section1Color);
                Debug.Log("[intent data] arguments : " + section2Color);
            }
            catch (Exception e)
            {
                Debug.Log("GameManager Exception : " + e.ToString());
            }

            // breed = "POME_LONG";
            // section1Color = "000000";

            switch (breed)
            {
                case "POME_LONG":
                    dogPrefab = pomeLongPrefab;
                    meshRenderer = dogPrefab.transform.Find("body").GetComponent<SkinnedMeshRenderer>();

                    if ((section1Color.CompareTo("ffffff") == 0) || (section1Color.CompareTo("f0ddcf") == 0)){
                        meshRenderer.material = pomeLong_white;
                    }
                    else if((section1Color.CompareTo("c68642") == 0) || (section1Color.CompareTo("945826") == 0)){
                        meshRenderer.material = pomeLong_brown;
                    }
                    else if(section1Color.CompareTo("000000") == 0){
                        meshRenderer.material = pomeLong_black;
                    }
                    break;
                case "POME_SHORT":
                    dogPrefab = pomeShortPrefab;
                    meshRenderer = dogPrefab.transform.Find("body").GetComponent<SkinnedMeshRenderer>();

                    if ((section1Color.CompareTo("ffffff") == 0) || (section1Color.CompareTo("f0ddcf") == 0)){
                        meshRenderer.material = pomeShort_white;
                    }
                    else if((section1Color.CompareTo("c68642") == 0) || (section1Color.CompareTo("945826" )==0)){
                        meshRenderer.material = pomeShort_brown;
                    }
                    else if(section1Color.CompareTo("000000") == 0){
                        meshRenderer.material = pomeShort_black;
                    }
                    break;
                case "MALTESE":
                    dogPrefab = maltesePrefab;
                    break;
                case "PUG":
                    dogPrefab = pugPrefab;
                    break;
                case "SHIHTZU":
                    dogPrefab = shihtzuPrefab;
                    break;
                case "GOLDEN":
                    dogPrefab = retrieverPrefab;
                    break;      
                 case "POODLE":
                    dogPrefab = poodlePrefab;
                    break;                                                        
                default:
                    dogPrefab = pomeShortPrefab;
                    break;
            }


            GameObject dog = Instantiate(dogPrefab, GameObject.Find("Character").transform) as GameObject;
            dog.transform.localScale = dogScale;
            dog.transform.localPosition = dogSummonPos;
            dog.tag = "OwnerDog";
            dog.layer = 6;

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

            DontDestroyOnLoad(CharacterParent);

        }
    }

    public void FurColor()
    {

    }

    public void FurLengthShort(bool isOn)
    {
        if (isOn){
            bodyfur_back.transform.localScale = shortFurValue_body;
            bodyfur_middle.transform.localScale = shortFurValue_body;
            bodyfur_front.transform.localScale = shortFurValue_body;

            chinfur.transform.localScale = shortFurValue_chin;
            neckfur.transform.localScale = shortFurValue_neck;
        }
    }


    public void FurLengthMiddle(bool isOn)
    {
        Debug.Log(isOn);
        if (isOn){
            bodyfur_back.transform.localScale = middleFurValue_body;
            bodyfur_middle.transform.localScale = middleFurValue_body;
            bodyfur_front.transform.localScale = middleFurValue_body;

            chinfur.transform.localScale = middleFurValue_chin;
            neckfur.transform.localScale = middleFurValue_neck;  
        }
    }

    public void FurLengthLong(bool isOn)
    {
        if (isOn){
            bodyfur_back.transform.localScale = longFurValue_body;
            bodyfur_middle.transform.localScale = longFurValue_body;
            bodyfur_front.transform.localScale = longFurValue_body;

            chinfur.transform.localScale = longFurValue_chin;
            neckfur.transform.localScale = longFurValue_neck;
        }
    }
}
