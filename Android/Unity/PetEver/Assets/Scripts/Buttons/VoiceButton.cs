using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class VoiceButton : MonoBehaviour
{

    private AndroidJavaObject activityContext = null;
    private AndroidJavaClass javaClass = null;
    private AndroidJavaObject javaClassInstance = null;

    public GameObject dimImageObject;
    public GameObject voiceBtnImageObject;
    public Sprite voiceBtnOverrideSprite;
    public Sprite voiceBtnOriginSprite;


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


    IEnumerator ShowAndHide(float delay)
    {
        dimImageObject.SetActive(true);
        voiceBtnImageObject.GetComponent<Image>().sprite = voiceBtnOverrideSprite;
        yield return new WaitForSeconds(delay);
        dimImageObject.SetActive(false);
        voiceBtnImageObject.GetComponent<Image>().sprite = voiceBtnOriginSprite;
    }
    public void HideImage()
    {
        dimImageObject.SetActive(false);
    }

    public void ShowImage()
    {
        StartCoroutine(ShowAndHide(2.0f));
    }

}
