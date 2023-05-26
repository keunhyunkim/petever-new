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
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {

        if (!("CreationScene".Equals(next.name)) && !("WorldScene".Equals(next.name)) && !("MySpaceScene".Equals(next.name)))
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
