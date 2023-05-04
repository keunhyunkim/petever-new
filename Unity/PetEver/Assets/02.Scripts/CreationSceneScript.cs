using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class CreationSceneScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup CustomPanel;
    [SerializeField] private CanvasGroup FurColorToggleGroup;
    [SerializeField] private CanvasGroup FurLengthToggleGroup;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickContinue()
    {
        LoadSceneManager.Instance.LoadScene("MySpaceScene");
    }
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

    IEnumerator<object> LoadYourAsyncScene()
    {
        string sceneName = "MySpaceScene";
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
