using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    GameObject chatUI;
    GameObject noticeUI;
    // Start is called before the first frame update
    void Start()
    {
        if (chatUI == null)
        {
            chatUI = GameObject.Find("Chat");
        }
        chatUI.SetActive(false);
        if (noticeUI == null)
        {
            noticeUI = GameObject.Find("Notice");
        }
        noticeUI.SetActive(true);
    }
    public void SetChatBarVisible()
    {
        if (chatUI & noticeUI)
        {
            if (chatUI.activeSelf == true)
            {
                chatUI.SetActive(false);
                noticeUI.SetActive(true);
            }
            else if (noticeUI.activeSelf == true)
            {
                noticeUI.SetActive(false);
                chatUI.SetActive(true);
            }
        } else{
            Debug.Log(chatUI);
            Debug.Log(noticeUI);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
