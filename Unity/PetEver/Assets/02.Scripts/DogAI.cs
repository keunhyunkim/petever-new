using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // add AI navigation system


public class DogAI : MonoBehaviour
{

    public LayerMask isPlayer;
    private GameObject owner; // tracing target
    private NavMeshAgent navMeshAgent; // assign navmeshagent component
    private Animator dogAnimator;

    // recognize player is on or not
    private bool hasOwner 
    {
        get
        {
            if (owner != null)
            {
                return true;
            }

            return false; 
        }
    }

    private void Awake()
    {
        dogAnimator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdatePath());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator UpdatePath()
    {
        while (true)
        {
            if (hasOwner)
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(owner.transform.position);
            }

            else
            {
                navMeshAgent.isStopped = true;
                Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, isPlayer);

                for (int i=0; i < colliders.Length; i++)
                {
                    GameObject gameObject = colliders[i].GetComponent<GameObject>();


                    if (gameObject != null)
                    {
                        owner = gameObject;
                        break; 
                    }
                }

            }

            yield return new WaitForSeconds(0.25f);
            Debug.Log(hasOwner);
        }
    }
}
