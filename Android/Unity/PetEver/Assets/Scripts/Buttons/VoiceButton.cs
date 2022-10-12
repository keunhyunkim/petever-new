using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceButton : MonoBehaviour
{

    private AndroidJavaObject activityContext = null;
    private AndroidJavaClass javaClass = null;
    private AndroidJavaObject javaClassInstance = null;


    void Awake()
    {

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
