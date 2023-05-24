using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Creation2WorldScene : MonoBehaviour
{
    private Scene scene;
    public RuntimeAnimatorController animatorController_creationScene;
    public RuntimeAnimatorController animatorController_worldScene_OwnerDog;
    public RuntimeAnimatorController animatorController_worldScene_DogNPC;


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
            gameObject.GetComponent<LineRenderer>().enabled = false;

            gameObject.layer = 0;

            if (gameObject.tag == "DogNPC")
            {
                gameObject.GetComponent<Animator>().runtimeAnimatorController = animatorController_worldScene_DogNPC;
                gameObject.GetComponent<DogEscort>().enabled = false;

            }
            else if(gameObject.tag == "OwnerDog")
            {
                gameObject.GetComponent<Animator>().runtimeAnimatorController = animatorController_worldScene_OwnerDog;
                gameObject.GetComponent<DogEscort>().enabled = true;               
            }           
        }  
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
            gameObject.GetComponent<LineRenderer>().enabled = false;


            gameObject.layer = 0;

            if (gameObject.tag == "DogNPC")
            {
                gameObject.GetComponent<Animator>().runtimeAnimatorController = animatorController_worldScene_DogNPC;
                gameObject.GetComponent<DogEscort>().enabled = false;

            }
            else if(gameObject.tag == "OwnerDog")
            {
                gameObject.GetComponent<Animator>().runtimeAnimatorController = animatorController_worldScene_OwnerDog;
                gameObject.GetComponent<DogEscort>().enabled = true;
            }

        }
    }
}
