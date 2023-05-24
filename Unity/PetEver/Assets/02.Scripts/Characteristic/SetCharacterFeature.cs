using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class SetCharacterFeature : MonoBehaviour
{
    private Vector3 dogSummonPos = new Vector3(-550f, 1070f, -10f);
    private Vector3 dogScale = new Vector3(60f, 60f, 60f);

    public GameObject dogPrefab;
    public GameObject dog;
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
    public Material pomeLong_black_deep;
    public Material pomeLong_black_light;
    public Material pomeLong_brown;
    public Material pomeLong_brown_deep;
    public Material pomeLong_brown_light;
    public Material pomeLong_white;
    public Material pomeLong_white_light;
    public Material pomeLong_white_deep;

    public Material pomeShort_black;
    public Material pomeShort_black_deep;
    public Material pomeShort_black_light;
    public Material pomeShort_brown;
    public Material pomeShort_brown_deep;
    public Material pomeShort_brown_light;
    public Material pomeShort_white;
    public Material pomeShort_white_deep;
    public Material pomeShort_white_light;


    public Material pug_black;
    public Material pug_black_deep;
    public Material pug_black_light;
    public Material pug_brown;
    public Material pug_brown_deep;
    public Material pug_brown_light;


    public Material maltese_white;
    public Material maltese_white_deep;
    public Material maltese_white_light;

    public Material shihtzu_whitebrown;
    public Material shihtzu_whitebrown_deep;
    public Material shihtzu_whitebrown_light;

    public Material retriever_brown;
    public Material retriever_brown_deep;
    public Material retriever_brown_light;


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
           // GameObject dogPrefab;
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

             //breed = "POME_LONG";
             //section1Color = "000000";

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
                    else
                    {
                        Debug.Log("No Color is matched with colorcode : "+ section1Color);
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
                    else
                    {
                        Debug.Log("No Color is matched with colorcode : "+ section1Color);
                    }                    
                    break;
                case "MALTESE":
                    dogPrefab = maltesePrefab;
                    break;
                case "PUG":
                    dogPrefab = pugPrefab;
                    meshRenderer = dogPrefab.transform.Find("body").GetComponent<SkinnedMeshRenderer>();
                    if (section1Color.CompareTo("000000") == 0){
                        meshRenderer.material = pug_black;
                    }
                    else if((section1Color.CompareTo("f4edde") == 0) || (section1Color.CompareTo("c69e7d" )==0)){
                        meshRenderer.material = pug_brown;    
                    }
                    else
                    {
                        Debug.Log("No Color is matched with colorcode : "+ section1Color);
                    }                                    
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


            dog = Instantiate(dogPrefab, GameObject.Find("Character").transform) as GameObject;
            dog.transform.localScale = dogScale;
            dog.transform.localPosition = dogSummonPos;
            dog.tag = "OwnerDog";
            dog.layer = 6;

            bodyfur_back = GameObject.Find("bodyfur_back");
            bodyfur_middle = GameObject.Find("bodyfur_middle");
            bodyfur_front = GameObject.Find("bodyfur_front");
            neckfur = GameObject.Find("neckfur");
            chinfur = GameObject.Find("chinfur");

            longFurValue_body = new Vector3(1.5f, 1.5f, 1.5f );
            middleFurValue_body = new Vector3(1.0f, 1.0f, 1.0f );
            shortFurValue_body = new Vector3(0.4f, 0.4f, 0.4f );

            longFurValue_neck = new Vector3(2.0f, 2.0f, 2.0f );
            middleFurValue_neck = new Vector3(1.0f, 1.0f, 1.0f );
            shortFurValue_neck = new Vector3(0.5f, 0.5f, 0.5f );

            longFurValue_chin = new Vector3(1.6f, 1.6f, 1.6f );
            middleFurValue_chin = new Vector3(1.0f, 1.0f, 1.0f );
            shortFurValue_chin = new Vector3(0.4f, 0.4f, 0.4f );   

            DontDestroyOnLoad(CharacterParent);

        }
    }

    public void FurColorLight(bool isOn)
    {
        if (isOn)
        {
            meshRenderer = dog.transform.Find("body").GetComponent<SkinnedMeshRenderer>();
            switch (breed)
            {
                case "POME_LONG":
                    if ((section1Color.CompareTo("ffffff") == 0) || (section1Color.CompareTo("f0ddcf") == 0)){
                        meshRenderer.material = pomeLong_white_light;
                    }
                    else if((section1Color.CompareTo("c68642") == 0) || (section1Color.CompareTo("945826") == 0)){
                        meshRenderer.material = pomeLong_brown_light;
                    }
                    else if(section1Color.CompareTo("000000") == 0){
                        meshRenderer.material = pomeLong_black_light;
                    }
                    else
                    {
                        Debug.Log("No Color is matched with colorcode : "+ section1Color);
                    }                    
                    break;
                case "POME_SHORT":
                    if ((section1Color.CompareTo("ffffff") == 0) || (section1Color.CompareTo("f0ddcf") == 0)){
                        meshRenderer.material = pomeShort_white_light;
                    }
                    else if((section1Color.CompareTo("c68642") == 0) || (section1Color.CompareTo("945826" )==0)){
                        meshRenderer.material = pomeShort_brown_light;
                    }
                    else if(section1Color.CompareTo("000000") == 0){
                        meshRenderer.material = pomeShort_black_light;
                    }
                    else
                    {
                        Debug.Log("No Color is matched with colorcode : "+ section1Color);
                    }                    
                    break;
                case "MALTESE":
                    meshRenderer.material = maltese_white_light;                    
                    break;
                case "PUG":
                    if (section1Color.CompareTo("000000") == 0){
                        meshRenderer.material = pug_black_light;
                    }
                    else if((section1Color.CompareTo("f4edde") == 0) || (section1Color.CompareTo("c69e7d" )==0)){
                        meshRenderer.material = pug_brown_light;       
                    }
                    else
                    {
                        Debug.Log("No Color is matched with colorcode : "+ section1Color);
                    }                                 
                    break;                   
                case "SHIHTZU":
                    meshRenderer.material = shihtzu_whitebrown_light;
                    break;
                case "GOLDEN":
                    meshRenderer.material = retriever_brown_light;
                    break;      
                 case "POODLE":
                    dogPrefab = poodlePrefab;
                    break;                                                        
                default:
                    dogPrefab = pomeShortPrefab;
                    break;
            }
        }
    }

    public void FurColorOrigin(bool isOn)
    {
        if (isOn)
        {
            meshRenderer = dog.transform.Find("body").GetComponent<SkinnedMeshRenderer>();
            switch (breed)
            {
                case "POME_LONG":
                    if ((section1Color.CompareTo("ffffff") == 0) || (section1Color.CompareTo("f0ddcf") == 0)){
                        meshRenderer.material = pomeLong_white;
                    }
                    else if((section1Color.CompareTo("c68642") == 0) || (section1Color.CompareTo("945826") == 0)){
                        meshRenderer.material = pomeLong_brown;
                    }
                    else if(section1Color.CompareTo("000000") == 0){
                        meshRenderer.material = pomeLong_black;
                    }
                    else
                    {
                        Debug.Log("No Color is matched with colorcode : "+ section1Color);
                    }                    
                    break;
                case "POME_SHORT":
                    if ((section1Color.CompareTo("ffffff") == 0) || (section1Color.CompareTo("f0ddcf") == 0)){
                        meshRenderer.material = pomeShort_white;
                    }
                    else if((section1Color.CompareTo("c68642") == 0) || (section1Color.CompareTo("945826" )==0)){
                        meshRenderer.material = pomeShort_brown;
                    }
                    else if(section1Color.CompareTo("000000") == 0){
                        meshRenderer.material = pomeShort_black;
                    }
                    else
                    {
                        Debug.Log("No Color is matched with colorcode : "+ section1Color);
                    }                    
                    break;
                case "MALTESE":
                    meshRenderer.material = maltese_white;                    
                    break;
                case "PUG":
                    if (section1Color.CompareTo("000000") == 0){
                        meshRenderer.material = pug_black;
                    }
                    else if((section1Color.CompareTo("f4edde") == 0) || (section1Color.CompareTo("c69e7d" )==0)){
                        meshRenderer.material = pug_brown;  
                    }
                    else
                    {
                        Debug.Log("No Color is matched with colorcode : "+ section1Color);
                    }                    
                    break;
                case "SHIHTZU":
                    meshRenderer.material = shihtzu_whitebrown;
                    break;
                case "GOLDEN":
                    meshRenderer.material = retriever_brown;
                    break;      
                 case "POODLE":
                    dogPrefab = poodlePrefab;
                    break;                                                        
                default:
                    dogPrefab = pomeShortPrefab;
                    break;
            }
        }
    }


    public void FurColorDeep(bool isOn)
    {
        if (isOn)
        {
            meshRenderer = dog.transform.Find("body").GetComponent<SkinnedMeshRenderer>();
            switch (breed)
            {
                case "POME_LONG":
                    if ((section1Color.CompareTo("ffffff") == 0) || (section1Color.CompareTo("f0ddcf") == 0)){
                        meshRenderer.material = pomeLong_white_deep;
                    }
                    else if((section1Color.CompareTo("c68642") == 0) || (section1Color.CompareTo("945826") == 0)){
                        meshRenderer.material = pomeLong_brown_deep;
                    }
                    else if(section1Color.CompareTo("000000") == 0){
                        meshRenderer.material = pomeLong_black_deep;
                    }
                    else
                    {
                        Debug.Log("No Color is matched with colorcode : "+ section1Color);
                    }                    
                    break;
                case "POME_SHORT":
                    if ((section1Color.CompareTo("ffffff") == 0) || (section1Color.CompareTo("f0ddcf") == 0)){
                        meshRenderer.material = pomeShort_white_deep;
                    }
                    else if((section1Color.CompareTo("c68642") == 0) || (section1Color.CompareTo("945826" )==0)){
                        meshRenderer.material = pomeShort_brown_deep;
                    }
                    else if(section1Color.CompareTo("000000") == 0){
                        meshRenderer.material = pomeShort_black_deep;
                    }
                    else
                    {
                        Debug.Log("No Color is matched with colorcode : "+ section1Color);
                    }                    
                    break;
                case "MALTESE":
                    meshRenderer.material = maltese_white_deep;                    
                    break;
                case "PUG":
                    if (section1Color.CompareTo("000000") == 0){
                        meshRenderer.material = pug_black_deep;
                    }
                    else if((section1Color.CompareTo("f4edde") == 0) || (section1Color.CompareTo("c69e7d" )==0)){
                        meshRenderer.material = pug_brown_deep;  
                    }
                    else
                    {
                        Debug.Log("No Color is matched with colorcode : "+ section1Color);
                    }                    
                    break;
                case "SHIHTZU":
                    meshRenderer.material = shihtzu_whitebrown_deep;
                    break;
                case "GOLDEN":
                    meshRenderer.material = retriever_brown_deep;
                    break;      
                 case "POODLE":
                    dogPrefab = poodlePrefab;
                    break;                                                        
                default:
                    dogPrefab = pomeShortPrefab;
                    break;
            }
        }
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
