using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MemorialSceneUIControl : MonoBehaviour
{
    [SerializeField] CanvasGroup stickyNoteInputPanel;
    [SerializeField] CanvasGroup photoPanel;
    [SerializeField] TMP_InputField stickyNoteInput;    
    string resourceUrl = "Prefabs/";
    GameObject owner;
    [SerializeField] GameObject wallArea;
    void Start()
    {
        owner = GameObject.FindGameObjectWithTag("Owner");
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

        createPostIt();
        // TODO :  save input text into server..
        stickyNoteInput.text = "";
        hideCanvasGroup(stickyNoteInputPanel);
    }

    void createPostIt(){
        GameObject postItPrefab = Resources.Load<GameObject>(resourceUrl + "postit");
       
        Vector3 forwardPos =  owner.transform.position + (owner.transform.forward * 3);
        Vector3 prefabPos = postItPrefab.transform.position;
        prefabPos.x = forwardPos.x;

        
        GameObject postIt = Instantiate(postItPrefab, prefabPos, postItPrefab.transform.rotation) as GameObject;

        Transform parent = TransformExtension.FindChildByRecursive(wallArea.transform, "PostIt");

        if (parent != null)
        {
            postIt.transform.SetParent(parent, false);
        }

    }
    void hideCanvasGroup(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
}
