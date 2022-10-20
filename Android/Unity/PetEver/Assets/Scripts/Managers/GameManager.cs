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

    public GameObject dogModel;
    public Animator anim;
    String breed = "";
    void Awake()
    {
        Vector3 dogScale = new Vector3(0.8f, 0.8f, 0.8f);

        using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            breed = "";
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
    // Start is called before the first frame update
    void Start()
    {
        dogModel = GameObject.FindGameObjectWithTag("Dog");
        anim = this.dogModel.GetComponent<Animator>();

    }


    // Update is called once per frame
    void Update()
    {
        

    }

    public void OnClicktakeWalk()
    {
        try
        {
            if (anim != null)
            {
                if(breed == "POME_SHORT")
                {
                    anim.Play("metarig|tilting");
                } 
                else if(breed =="POME_LONG")
                {
                    anim.Play("metarig|feetup_2");
                }
                else
                {
                    anim.Play("metarig|feetup_2");
                }

            }

        }
        catch (Exception e)
        {
            Debug.Log("OnClicktakeWalk Exception : " + e.ToString());
        }
    }

    public void OnClickTreat()
    {
        try
        {
            if (anim != null)
            {
                anim.Play("metarig|idle_2_sniffing");
            }

        }
        catch (Exception e)
        {
            Debug.Log("OnClickTreat Exception : " + e.ToString());
        }
    }
    public void OnClickCheerUp()
    {
        try
        {
            if (anim != null)
            {
                anim.Play("metarig|tailing");
            }
            
        }
        catch (Exception e)
        {
            Debug.Log("OnClickCheerUp Exception : " + e.ToString());
        }
    }

    public void OnClickCharacter()
    {
        try
        {
            if (anim != null)
            {
                anim.Play("metarig|givehand");
            }

        }
        catch (Exception e)
        {
            Debug.Log("OnClickCharacter Exception : " + e.ToString());
        }
    }
}
