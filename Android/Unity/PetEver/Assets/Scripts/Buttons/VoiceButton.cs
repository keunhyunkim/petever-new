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
    private AndroidJavaClass javaClass = null;
    private AndroidJavaObject javaClassInstance = null;

    public GameObject dimImageObject;
    public GameObject voiceBtnImageObject;
    public GameObject voiceText;
    private TextMeshProUGUI tmpText;

    public Sprite voiceBtnOverrideSprite;
    public Sprite voiceBtnOriginSprite;

    void Awake()
    {
        HideImage();
        

        try
        {
            using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
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

    private void Start() {
        tmpText = voiceText.GetComponent<TextMeshProUGUI>();
    }
     
    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnClickVoiceButton()
    {
        ShowImage();

        if (javaClass != null && activityContext != null)
        {
            javaClass.CallStatic("startVoiceRecognition", activityContext);
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
    }


    IEnumerator ShowAndHide()
    {
        dimImageObject.SetActive(true);
        voiceBtnImageObject.GetComponent<Image>().sprite = voiceBtnOverrideSprite;
        yield return new WaitForSeconds(1.5f);
        
        try
        {
            returnVoiceStr = javaClass.Call<String>("returnVoiceStr", "");
            tmpText.text = returnVoiceStr;
        }
        catch (Exception e)
        {
            Debug.Log("Exception " + e.ToString());
        }
        voiceText.SetActive(true);

        yield return new WaitForSeconds(2.0f);
        HideImage();
    }
    public void HideImage()
    {
        returnVoiceStr = "";

        dimImageObject.SetActive(false);
        voiceText.SetActive(false);
        
        voiceBtnImageObject.GetComponent<Image>().sprite = voiceBtnOriginSprite;
    }

    public void ShowImage()
    {
        StartCoroutine(ShowAndHide());
    }

}
