using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI; // add AI navigation system


public class DogSetting : MonoBehaviour
{
    public GameObject OwnerDog;
    private Scene scene;
    private LineRenderer lr;
    private SphereCollider sc;
    private NavMeshAgent nav;
    [SerializeField] private Material defaultline;
    public RuntimeAnimatorController animatorController_creationScene;
    public RuntimeAnimatorController animatorController_worldScene_OwnerDog;
    public RuntimeAnimatorController animatorController_worldScene_DogNPC;


    Color c1 = Color.white;
    Color c2 = Color.white;

    // Start is called before the first frame update
    void Awake()
    {
        scene = SceneManager.GetActiveScene();
        OwnerDog = GameObject.FindGameObjectWithTag("OwnerDog");
    }


    
    void Start()
    {
        OwnerDog = GameObject.FindGameObjectWithTag("OwnerDog");
        nav = OwnerDog.GetComponent<NavMeshAgent>();

        if ("CreationScene".Equals(scene.name))
        {
            nav.enabled = false;
            OwnerDog.GetComponent<Animator>().runtimeAnimatorController = OwnerDog.GetComponent<AnimatorSetup>().animatorController_creationScene;
            OwnerDog.GetComponent<DogAI>().enabled = false;

        }

        else if ("WorldScene".Equals(scene.name))
        {
            OwnerDog.AddComponent<SphereCollider>();
            OwnerDog.AddComponent<GetColliderScript>();
            OwnerDog.AddComponent<DogEscort>();
            OwnerDog.AddComponent<LineRenderer>();
            OwnerDog.AddComponent<Creation2WorldScene>();


            OwnerDog.layer = 0;
            OwnerDog.GetComponent<Animator>().runtimeAnimatorController = OwnerDog.GetComponent<AnimatorSetup>().animatorController_worldScene_OwnerDog;

            //LineRenderer option setting
            lr = OwnerDog.GetComponent<LineRenderer>();
            lr.startColor = c1;
            lr.endColor = c2;
            lr.startWidth = 1.5f;
            lr.endWidth = 1.5f;
            
            lr.material = defaultline;

            //SphereCollider option setting
            sc = OwnerDog.GetComponent<SphereCollider>();
            sc.radius = 6f;
            sc.isTrigger = true; 

            //NavMeshAgent option setting
            nav.enabled = true;
            OwnerDog.GetComponent<DogAI>().enabled = true;

        }

    }
}