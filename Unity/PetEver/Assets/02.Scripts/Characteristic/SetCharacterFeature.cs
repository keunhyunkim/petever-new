using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class SetCharacterFeature : MonoBehaviour
{
    private Vector3 dogSummonPos = new Vector3(-0.21f, -1.66f, -0.24f);
    private Vector3 dogScale = new Vector3(2f, 2f, 2f);

    public GameObject maltesePrefab;
    public GameObject pomeLongPrefab;
    public GameObject pomeShortPrefab;
    public GameObject pugPrefab;

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
                default:
                    dogPrefab = pomeShortPrefab;
                    break;
            }


            GameObject dog = Instantiate(dogPrefab, GameObject.Find("Character").transform) as GameObject;
            dog.transform.localScale = dogScale;
            dog.transform.localPosition = dogSummonPos;

        }
    }
}
