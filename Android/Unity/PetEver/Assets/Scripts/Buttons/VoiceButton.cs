using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class VoiceButton : MonoBehaviour
{

    private AndroidJavaObject activityContext = null;
    private String returnVoiceStr = null;
    private AndroidJavaClass javaClass = null;
    private AndroidJavaObject javaClassInstance = null;

    public GameObject dimImageObject;
    public GameObject voiceBtnImageObject;
    public GameObject voiceText;
    public Sprite voiceBtnOverrideSprite;


    void Awake()
    {
        HideImage();

        using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
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
            voiceText.GetComponent<Text>().text = returnVoiceStr;
        }
        catch (Exception e){
            Debug.Log("Exception " + e.ToString());
        }
        voiceText.SetActive(true);
    }
    public void HideImage()
    {
        returnVoiceStr = "";

        dimImageObject.SetActive(false);
        voiceText.SetActive(false);
    }

    public void ShowImage()
    {
        StartCoroutine(ShowAndHide());
    }

}
