using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySpaceCanvasScript : MonoBehaviour
{

    [SerializeField] private CanvasGroup questPopupPanel;
    [SerializeField] private CanvasGroup questDetailPopup;
    [SerializeField] private CanvasGroup worldScenePopup;
    [SerializeField] private GameObject questSelectionLight;

    public void onClickQuestBtn()
    {
        questSelectionLight.SetActive(true);
        showCanvasGroup(questPopupPanel);
    }
    public void onClickCloseBtn()
    {
        hideCanvasGroup(questPopupPanel);
        questSelectionLight.SetActive(false);
    }
    public void onClickQuestItem()
    {
        showCanvasGroup(questDetailPopup);
    }
    public void onClickQuestDetailComplete()
    {
        hideCanvasGroup(questDetailPopup);
    }
    public void onClickGoToWorldScene()
    {
        //StartCoroutine(LoadYourAsyncScene());
        
        LoadSceneManager.Instance.LoadScene("WorldScene");
    }
    public void onClickWorldScene()
    {
        showCanvasGroup(worldScenePopup);
    }
    public void onClickCloseWorldScenePopupBtn()
    {
        hideCanvasGroup(worldScenePopup);
    }
    IEnumerator<object> LoadYourAsyncScene()
    {
        string sceneName = "WorldScene";
        // Set the current Scene to be able to unload it later
        Scene currentScene = SceneManager.GetActiveScene();

        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }


        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);
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
