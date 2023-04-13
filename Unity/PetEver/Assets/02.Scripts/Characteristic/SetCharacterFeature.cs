using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class SetCharacterFeature : MonoBehaviour
{
    public GameObject maltesePrefab;
    public GameObject pomeLongPrefab;
    public GameObject pomeShortPrefab;

    private String breed = "";
    private String petName = "";
    private String section1Color = "";
    private String section2Color = "";
    private AndroidJavaObject activityContext = null;

    void Awake()
    {
        Vector3 dogScale = new Vector3(0.8f, 0.8f, 0.8f);

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

            switch (breed)
            {
                case "POME_LONG":
                    dogPrefab = pomeLongPrefab;
                    break;
                case "POME_SHORT":
                    dogPrefab = pomeShortPrefab;
                    break;
                case "MALTESE":
                    dogPrefab = maltesePrefab;
                    break;
                default:
                    dogPrefab = pomeShortPrefab;
                    break;
            }

            GameObject dog = Instantiate(dogPrefab, GameObject.Find("Object Parent").transform) as GameObject;
            dog.transform.localScale = dogScale;
        }
    }
}
