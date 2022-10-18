using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");

            AndroidJavaObject intent = currentActivity.Call<AndroidJavaObject>("getIntent");

            String arguments = intent.Call<string>("getStringExtra", "breed");

            Debug.Log("[breed] arguments : " + arguments);
        }
       
    }

    // Update is called once per frame
    void Update()
    {

    }
}
