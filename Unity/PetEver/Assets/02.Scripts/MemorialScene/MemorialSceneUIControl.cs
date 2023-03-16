using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MemorialSceneUIControl : MonoBehaviour
{
    CanvasGroup stickyNoteInputPanel;
    CanvasGroup photoPanel;
    TMP_InputField stickyNoteInput;
    void Start()
    {
        stickyNoteInputPanel = GameObject.Find("StickyNotePanel").GetComponent<CanvasGroup>();
        photoPanel = GameObject.Find("PhotoPanel").GetComponent<CanvasGroup>();
        stickyNoteInput = GameObject.Find("StickyNoteInput").GetComponent<TMP_InputField>();

    }
    
    public void OnExitClicked()
    {
        hideCanvasGroup(stickyNoteInputPanel);
    }    
    public void OnExitPhotoPanelClicked()
    {
        hideCanvasGroup(photoPanel);
    }
    public void OnCompleteClicked()
    {
        // TODO :  save input text into server..
        
        hideCanvasGroup(stickyNoteInputPanel);
    }
    void hideCanvasGroup(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
}
