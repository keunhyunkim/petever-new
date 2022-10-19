using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private AndroidJavaObject activityContext = null;
    public GameObject maltesePrefab;
    public GameObject pomeLongPrefab;
    public GameObject pomeShortPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 dogScale = new Vector3(1.0f, 1.0f, 1.0f);

        using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            String breed = "";
            GameObject dogPrefab;
            try
            {
                activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");

                AndroidJavaObject intent = activityContext.Call<AndroidJavaObject>("getIntent");

                breed = intent.Call<String>("getStringExtra", "breed");
                String petName = intent.Call<String>("getStringExtra", "petname");

                Debug.Log("[intent data] arguments : " + breed);
                Debug.Log("[intent data] arguments : " + petName);
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
                    dogPrefab = pomeLongPrefab;
                    break;
            }

            GameObject dog = Instantiate(dogPrefab, GameObject.Find("Object Parent").transform) as GameObject;
            dog.transform.localScale = dogScale;
        }
    }
        }
        catch (Exception e)
        {
            Debug.Log("GameManager Exception : " + e.ToString());
        }


        //GameObject dog = Instantiate(pomeShortPrefab, GameObject.Find("Object Parent").transform) as GameObject;
        //dog.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
