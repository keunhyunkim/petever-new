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

    public Image dimImage;

    public void HideImage()
    {
        dimImage.enabled = false;

    }

    public void ShowImage()
    {
        dimImage.enabled = true;

    }
    void Awake()
    {
        GameObject imageObject = GameObject.FindGameObjectWithTag("DimImage");
        if (imageObject != null)
        {
            dimImage = imageObject.GetComponent<Image>();
        }

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
        Debug.Log("OnClickVoiceButton!!");
        if (javaClass != null && activityContext != null)
        {
            javaClass.CallStatic("startVoiceRecognition", activityContext);
        }
        else
        {
            Debug.Log("javaClass or activityContext null");

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

}
