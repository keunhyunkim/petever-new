using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySpaceCanvasScript : MonoBehaviour
{

    public CanvasGroup questPopupPanel;
    public CanvasGroup questDetailPopup;
    public CanvasGroup worldScenePopup;
    public GameObject questSelectionLight;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
        StartCoroutine(LoadYourAsyncScene());
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
