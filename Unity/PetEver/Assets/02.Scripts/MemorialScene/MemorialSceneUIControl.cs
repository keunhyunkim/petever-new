using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MemorialSceneUIControl : MonoBehaviour
{
    CanvasGroup stickyNoteInputPanel;
    TMP_InputField stickyNoteInput;
    void Start()
    {
        stickyNoteInputPanel = GameObject.Find("StickyNotePanel").GetComponent<CanvasGroup>();
        stickyNoteInput = GameObject.Find("StickyNoteInput").GetComponent<TMP_InputField>();

    }
    
    public void OnExitClicked()
    {
        hideCanvasGroup(stickyNoteInputPanel);
    }
    public void OnCompleteClicked()
    {
        // TODO :  save input text into server..
        
        hideCanvasGroup(stickyNoteInputPanel);
    }
    public void OnStickyNoteInputOpenClicked()
    {
        showCanvasGroup(stickyNoteInputPanel);
    }
    void showCanvasGroup(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    void hideCanvasGroup(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }


}
