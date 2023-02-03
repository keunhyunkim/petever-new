using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackWorldScene : MonoBehaviour
{
    GameObject ManCharacter;
    GameObject MainCanvas;
    GameObject MainEvent;
    private bool isEntered = false;
    void Start()
    {
        ManCharacter = GameObject.FindGameObjectWithTag("Owner");
        MainCanvas = GameObject.FindGameObjectWithTag("UICanvas");
        MainEvent = GameObject.FindGameObjectWithTag("MainEventSystem");
    }

    IEnumerator<object> GoWorldScene()
    {
        string sceneName = "WorldScene";
        
        Scene currentScene = SceneManager.GetActiveScene();
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
 
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        
        SceneManager.MoveGameObjectToScene(ManCharacter, SceneManager.GetSceneByName(sceneName));
        SceneManager.MoveGameObjectToScene(MainEvent, SceneManager.GetSceneByName(sceneName));
        SceneManager.MoveGameObjectToScene(MainCanvas, SceneManager.GetSceneByName(sceneName));
        SceneManager.UnloadSceneAsync(currentScene);
        
       
    }

    private void OnCollisionEnter(Collision collision)
    { 
         if (collision.gameObject.tag == "Owner")
        {
            if (isEntered == false)
            {
                StartCoroutine(GoWorldScene());
                isEntered = true;
            }
        }
    }
}
