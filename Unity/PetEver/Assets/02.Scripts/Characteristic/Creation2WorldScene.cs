using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Creation2WorldScene : MonoBehaviour
{
    private Scene scene;

    // Start is called before the first frame update
    void Awake()
    {

    }
    
    void Start(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (!("CreationScene".Equals(scene.name)) && !("WorldScene".Equals(scene.name)) && !("MySpaceScene".Equals(scene.name)))
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
