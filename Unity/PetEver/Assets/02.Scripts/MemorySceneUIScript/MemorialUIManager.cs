using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MemorialUIManager : MonoBehaviour
{
    public CanvasGroup NewpageUI;

    [SerializeField] private GameObject MemorialSceneCanvas;
    [SerializeField] private Button LoadNewButton;

    // public UnityAction action;

    // Start is called before the first frame update
    void Start()
    {
        LoadNewButton.onClick.AddListener(delegate { LoadNewpage(); });
    }



    public void LoadNewpage()
    {
        ShowCanvasGroup(NewpageUI);

    }

    private void ShowCanvasGroup(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
}
