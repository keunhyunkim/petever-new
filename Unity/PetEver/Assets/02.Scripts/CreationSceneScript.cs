using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class CreationSceneScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup CustomPanel;
    [SerializeField] private CanvasGroup FurColorToggleGroup;
    [SerializeField] private CanvasGroup FurLengthToggleGroup;

    public void OnClickCustomBtn()
    {
        showCanvasGroup(CustomPanel);
    }
    public void OnClickCompleteCustomBtn()
    {
        // save custom data
        hideCanvasGroup(CustomPanel);
    }
    public void OnClickFurColor()
    {
        hideCanvasGroup(FurLengthToggleGroup);
        showCanvasGroup(FurColorToggleGroup);
    }
    public void OnClickFurLength()
    {
        hideCanvasGroup(FurColorToggleGroup);
        showCanvasGroup(FurLengthToggleGroup);
    }

    public void OnClickRemake()
    {
        Application.Quit();
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
