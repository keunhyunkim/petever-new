using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* reference : https://jacobjea.tistory.com/7 */
public class ShowHouseName : MonoBehaviour
{
    /** SHOULD BE CHANGED **/

    /* House Information */
    private string houseName = "병원정보\n공유의집";
    private string bubbleName = "SpeechBubble";
    private string bubbleTxtName = "BubbleText";
    /*********************/

    GameObject bubble;
    GameObject bubbleBox;
    GameObject bubbleTxtBox;
    TextMeshProUGUI bubbleTxt;

    void Start() {
        bubble = GameObject.FindGameObjectWithTag(bubbleName);
        bubbleBox = GameObject.Find("BubbleImage");
        bubbleTxtBox = GameObject.Find(bubbleTxtName);
        bubbleTxt = GameObject.Find(bubbleTxtName).GetComponent<TextMeshProUGUI>();
        bubbleBox.SetActive(false);
        bubble.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        bubble.SetActive(true);
        bubbleBox.SetActive(true);
        bubbleTxt.text = houseName;
        bubbleTxtBox.SetActive(true);
    }

    private void OnCollisionExit(Collision collision)
    {
        bubbleTxtBox.SetActive(false);
        bubbleBox.SetActive(false);
        bubble.SetActive(false);
    }
}
