using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VoiceButton : MonoBehaviour
{

    private AndroidJavaObject activityContext = null;
    private String returnVoiceStr = null;
    private String originVoiceStr = null;
    private AndroidJavaClass javaClass = null;
    private AndroidJavaObject javaClassInstance = null;

    public GameObject dimImageObject;
    public GameObject voiceBtnImageObject;
    public GameObject voiceText;
    private TextMeshProUGUI tmpText;
    public GameObject statusBarText;
    private TextMeshProUGUI tmpStatusBarText;

    public GameObject dogModel;
    public Animator anim;

    public Sprite voiceBtnOverrideSprite;
    public Sprite voiceBtnOriginSprite;

    private bool getVoiceFlag = false;
    private bool voiceBtnClicked = false;


    public GameObject heartPrefab;

    GameObject heart;


    bool heartAnimate = false;



    String breed = "";
    String petName = "";
    void Awake()
    {
        HideImage();


        try
        {
            using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
                AndroidJavaObject intent = activityContext.Call<AndroidJavaObject>("getIntent");
                breed = intent.Call<String>("getStringExtra", "breed");
                petName = intent.Call<String>("getStringExtra", "petname");

                Debug.Log("[intent data] arguments : " + breed);
                Debug.Log("[intent data] arguments : " + petName);


            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }


        javaClass = new AndroidJavaClass("com.example.petevervoice.PetEverVoice");

        if (javaClass != null && activityContext != null)
        {
            try
            {
                javaClassInstance = javaClass.CallStatic<AndroidJavaObject>("getInstance", activityContext);
            }
            catch (Exception e)
            {
                Debug.Log("Exception " + e.ToString());
            }

        }

    }

    private void Start()
    {
        tmpText = voiceText.GetComponent<TextMeshProUGUI>();
        tmpStatusBarText = statusBarText.GetComponent<TextMeshProUGUI>();

        dogModel = GameObject.FindGameObjectWithTag("Dog");
        anim = this.dogModel.GetComponent<Animator>();
    }

    private float movementSpeed = 2f;
    // Update is called once per frame
    void Update()
    {
        if (voiceBtnClicked == true)
        {
            Debug.Log("[DEBUG TEST] Update : voiceBtnClickeds");
            if (javaClass != null && activityContext != null)
            {
                getVoiceFlag = javaClass.CallStatic<bool>("getVoiceFlag");
                Debug.Log("getVoiceFlag : " + getVoiceFlag);
                if (getVoiceFlag == true)
                {
                    try
                    {
                        originVoiceStr = javaClass.CallStatic<String>("getVoiceOriginStr");
                        Debug.Log("[DEBUG TEST] getVoiceOriginStr() : " + originVoiceStr);

                        returnVoiceStr = javaClass.CallStatic<String>("getVoiceResultStr");
                        Debug.Log("[DEBUG TEST] getVoiceResultStr() : " + returnVoiceStr);

                        if (returnVoiceStr != null)
                        {
                            processReturnVoiceStr(returnVoiceStr);
                        }

                        tmpText.text = "\"" + originVoiceStr + "\"";
                    }
                    catch (Exception e)
                    {
                        Debug.Log("Exception " + e.ToString());
                    }
                    voiceText.SetActive(true);

                    voiceBtnClicked = false;
                }
            }
            else
            {
                Debug.Log("[DEBUG TEST] Update javaClass activityContext Null");
            }
        }

        if (heartAnimate == true)
        {
            if (heart.transform.position.y < 2)
            {
                heart.transform.position = heart.transform.position + new Vector3(0, 1 * movementSpeed * Time.deltaTime, 0);
            }
            else
            {
                heartAnimate = false;
                heart.SetActive(false);
            }
        }
    }

    public void processReturnVoiceStr(String retStr)
    {
        switch (retStr)
        {
            case "기다려":
                anim.Play("metarig|sit");
                break;
            case "산책":
                OnClicktakeWalk();
                break;
            case "잘했어":
                anim.Play("metarig|tailing");
                break;
            default:
                anim.Play("metarig|tilting");
                break;
        }
    }

    public void CreateHeart()
    {
        heart = Instantiate(heartPrefab, GameObject.Find("Hearts").transform) as GameObject;
        heartAnimate = true;
    }

    public void OnClicktakeWalk()
    {
        try
        {
            if (anim != null)
            {
                if (breed == "POME_SHORT")
                {
                    anim.Play("metarig|feetup_2");
                }
                else if (breed == "POME_LONG")
                {
                    anim.Play("metarig|tilting");
                }
                else
                {
                    anim.Play("metarig|tilting");
                }
                CreateHeart();
            }
            HideImage();
        }
        catch (Exception e)
        {
            Debug.Log("OnClicktakeWalk Exception : " + e.ToString());
        }
    }
    public void OnClickVoiceButton()
    {
        Debug.Log("[DEBUG TEST] OnClickVoiceButton()");

        if (javaClass != null && activityContext != null)
        {

            Debug.Log("[DEBUG TEST] startVoiceRecognition()");
            javaClass.CallStatic("startVoiceRecognition", activityContext);
            voiceBtnClicked = true;
        }
        else
        {
            if (javaClass == null)
            {
                Debug.Log("javaClass null");
            }
            else if (activityContext == null)
            {
                Debug.Log("activityContext null");
            }
        }
        ShowImage();
    }


    IEnumerator ShowAndHide()
    {
        dimImageObject.SetActive(true);
        voiceBtnImageObject.GetComponent<Image>().sprite = voiceBtnOverrideSprite;


        yield return new WaitForSeconds(4.0f);
        HideImage();

    }
    public void HideImage()
    {

        Debug.Log("[DEBUG TEST] HideImage()");
        originVoiceStr = "";

        dimImageObject.SetActive(false);
        voiceText.SetActive(false);

        voiceBtnImageObject.GetComponent<Image>().sprite = voiceBtnOriginSprite;
    }

    public void ShowImage()
    {
        StartCoroutine(ShowAndHide());
    }

}
