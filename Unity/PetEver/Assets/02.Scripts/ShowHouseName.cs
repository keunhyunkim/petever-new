using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* reference : https://jacobjea.tistory.com/7 */
public class ShowHouseName : MonoBehaviour
{
    /**
     * Informations like below should be filled in order into Component<Text> of the house
     * if want to show the house info.
     *
     * <Information order>
     * House Title,Bubble Group Name,Bubble Text Name
    **/

    private CanvasGroup bubbleGroup;
    private GameObject bubbleTxtBox;
    private TextMeshProUGUI bubbleTxt;
    private string[] houseInfo;

    private void parsingHouseInfo(string info)
    {
        houseInfo = info.Split(',');
    }

    private void ShowBubble()
    {
        bubbleGroup.alpha = 1;
        bubbleGroup.interactable = true;
        bubbleGroup.blocksRaycasts = true;
    }

    private void HideBubble()
    {
        bubbleGroup.alpha = 0;
        bubbleGroup.interactable = false;
        bubbleGroup.blocksRaycasts = false;
    }

    void Start() {
        parsingHouseInfo(GetComponent<Text>().text);

        bubbleGroup = GameObject.Find(houseInfo[1]).GetComponent<CanvasGroup>();

        bubbleTxtBox = GameObject.Find(houseInfo[2]);
        bubbleTxt = bubbleTxtBox.GetComponent<TextMeshProUGUI>();

        HideBubble();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Owner") {
            ShowBubble();
            bubbleTxt.text = houseInfo[0];
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Owner") {
            HideBubble();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Owner") {
            ShowBubble();
            bubbleTxt.text = houseInfo[0];
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Owner") {
            HideBubble();
        }
    }
}
