using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class CreationSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickContinue()
    {
        StartCoroutine(LoadYourAsyncScene());
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



}
