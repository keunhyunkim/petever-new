using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CallCanvas : MonoBehaviour
{
    GameObject ManCharacter;
    GameObject mainCanvas;
    GameObject mainEventSystem;

    void Start()
    {
        ManCharacter = GameObject.Find("Man");
        mainCanvas = GameObject.Find("MainCanvas");
        mainEventSystem = GameObject.Find("MainEventSystem");

        ManCharacter.SetActive(true);
        mainCanvas.SetActive(true);
        mainEventSystem.SetActive(true);
    }

    public void LoadScene(string drawCanvas)
    {
        StartCoroutine(LoadYourAsyncScene(drawCanvas));
    }

    IEnumerator<object> LoadYourAsyncScene(string drawCanvas)
    {
        Scene currentScene = SceneManager.GetActiveScene();
 
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(drawCanvas, LoadSceneMode.Additive);
 
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
 
        SceneManager.MoveGameObjectToScene(ManCharacter, SceneManager.GetSceneByName(drawCanvas));
        SceneManager.MoveGameObjectToScene(mainEventSystem, SceneManager.GetSceneByName(drawCanvas));
        SceneManager.MoveGameObjectToScene(mainCanvas, SceneManager.GetSceneByName(drawCanvas));

        SceneManager.UnloadSceneAsync(currentScene);
    }
 
    private void OnCollisionEnter(Collision collision)
    {
        LoadScene("CanvasScene");
    }
}
