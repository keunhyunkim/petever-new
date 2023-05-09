using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatInputManager : MonoBehaviour
{
    public TMP_InputField chatInput;
    public GameObject chatTextPrefab;

    public void InputTextFinished()
    {
        if (chatInput.text != null)
        {
            GameObject myInstance = Instantiate(chatTextPrefab, GameObject.Find("Content").transform);

            TextMeshProUGUI mText = myInstance.GetComponent<TextMeshProUGUI>();
            if (mText != null)
            {
                mText.text = chatInput.text;
                chatInput.text = "";
            }
            else
            {

            }
        }
    }
}
