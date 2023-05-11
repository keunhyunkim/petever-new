using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Creation2WorldScene : MonoBehaviour
{
    private Scene scene;
    public RuntimeAnimatorController animatorController_creationScene;
    public RuntimeAnimatorController animatorController_worldScene;

    // Start is called before the first frame update
    void Awake()
    {
        scene = SceneManager.GetActiveScene();
        if ("CreationScene".Equals(scene.name))
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            gameObject.GetComponent<GetColliderScript>().enabled = false;
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            gameObject.GetComponent<DogAI>().enabled = false;
            gameObject.GetComponent<DogEscort>().enabled = false;
            gameObject.GetComponent<LineRenderer>().enabled = false;

            gameObject.GetComponent<Creation2WorldScene>().enabled = true;


            gameObject.layer = 6;
            gameObject.GetComponent<Animator>().runtimeAnimatorController = animatorController_creationScene;
        }
        else if ("WorldScene".Equals(scene.name))
        {
            gameObject.GetComponent<SphereCollider>().enabled = true;
            gameObject.GetComponent<CapsuleCollider>().enabled = true;
            gameObject.GetComponent<GetColliderScript>().enabled = true;
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            gameObject.GetComponent<DogAI>().enabled = true;
            gameObject.GetComponent<DogEscort>().enabled = true;
            gameObject.GetComponent<LineRenderer>().enabled = true;

            GameObject.Find("Characteristic").GetComponent<DogInteraction>().enabled = false;

            gameObject.layer = 0;
            gameObject.GetComponent<Animator>().runtimeAnimatorController = animatorController_worldScene;

        }
        
        
    }
    void Start(){
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }
    private void ChangedActiveScene(Scene current, Scene next)
    {
        Debug.Log(current.name + ", "+ next.name);

        if ("CreationScene".Equals(next.name))
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            gameObject.GetComponent<GetColliderScript>().enabled = false;
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            gameObject.GetComponent<DogAI>().enabled = false;
            gameObject.GetComponent<DogEscort>().enabled = false;
            gameObject.GetComponent<LineRenderer>().enabled = false;

            gameObject.GetComponent<Creation2WorldScene>().enabled = true;


            gameObject.layer = 6;
            gameObject.GetComponent<Animator>().runtimeAnimatorController = animatorController_creationScene;
        }
        else if ("WorldScene".Equals(next.name))
        {
            gameObject.GetComponent<SphereCollider>().enabled = true;
            gameObject.GetComponent<CapsuleCollider>().enabled = true;
            gameObject.GetComponent<GetColliderScript>().enabled = true;
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            gameObject.GetComponent<DogAI>().enabled = true;
            gameObject.GetComponent<DogEscort>().enabled = true;
            gameObject.GetComponent<LineRenderer>().enabled = true;

            GameObject.Find("Characteristic").GetComponent<DogInteraction>().enabled = false;

            gameObject.layer = 0;
            gameObject.GetComponent<Animator>().runtimeAnimatorController = animatorController_worldScene;

        }
        
    }
}
