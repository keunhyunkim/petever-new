using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackWorldScene : MonoBehaviour
{
    GameObject ManCharacter;
    GameObject MainCanvas;
    GameObject MainEvent;

    void Start()
    {
        ManCharacter = GameObject.FindGameObjectWithTag("Owner");
        MainCanvas = GameObject.FindGameObjectWithTag("UICanvas");
        MainEvent = GameObject.Find("MainEventSystem");
    }

    IEnumerator<object> GoWorldScene(string SceneName)
    {
        
        Scene currentScene = SceneManager.GetActiveScene();
 
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);
 
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        
        SceneManager.MoveGameObjectToScene(ManCharacter, SceneManager.GetSceneByName(SceneName));
        SceneManager.MoveGameObjectToScene(MainEvent, SceneManager.GetSceneByName(SceneName));
        SceneManager.MoveGameObjectToScene(MainCanvas, SceneManager.GetSceneByName(SceneName));
        SceneManager.UnloadSceneAsync(currentScene);
        
       
    }

    private void OnCollisionEnter(Collision collision)
    { 
        Debug.Log("Collision has enter");
        StartCoroutine(GoWorldScene("WorldScene"));
    }
}
