using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CallCanvas : MonoBehaviour
{
    private bool isEntered = false;
    public static List<string> sceneHistory = null;  //running history of scenes
    //The last string in the list is always the current scene running

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    IEnumerator<object> LoadCanvas()
    {
        // Set the current Scene to be able to unload it later
        Scene currentScene = SceneManager.GetActiveScene();
 
        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("CanvasScene", LoadSceneMode.Additive);
 
        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
 
        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);
    }

    //Call this whenever you want to load a new scene
    //It will add the new scene to the sceneHistory list
    public void LoadScene(string newScene)
    {
        sceneHistory.Add(newScene);
        SceneManager.LoadScene(newScene);
    }
 
    private void OnCollisionEnter(Collision collision)
    {
    
        if (sceneHistory == null) {
            sceneHistory = new List<string>();  //running history of scenes

            Scene currentScene = SceneManager.GetActiveScene();
            sceneHistory.Add(currentScene.name);
        }

        if (isEntered == false) {   
                // StartCoroutine(LoadCanvas());
                LoadScene("CanvasScene");
                isEntered = true;
        }
        }
}
