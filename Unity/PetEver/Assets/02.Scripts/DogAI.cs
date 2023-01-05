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

    private float range = 20f;
    private Vector3 point;
    private Vector3 lastpos;
    private bool arrived = true;
    private bool trackingOwner = false;
    private float timer = 0f;


    // recognize player is on or not

    private bool meetOwner
    {
        get
        {
            if ((gameObject.transform.position - owner.transform.position).magnitude < 4f)
            {
                return true;
            }

            return false;
        }
    }

    private bool walking
    {
        get
        {
            if (!(gameObject.transform.position.x == lastpos.x))
            {
                return true;
            }
            return false;
        }
    }


    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range; // take random point from 'range' radious sphere around center
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }



    void Awake()
    {
        dogAnimator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        owner = GameObject.FindGameObjectWithTag("Owner");

    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdatePath());
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        TrackingOwner();
        DogAnimation();

        lastpos = gameObject.transform.position;


    }

    // randomly dog moves 
    IEnumerator UpdatePath()
    {
        while (true)
        {
            if (arrived)
            {
                arrived = false;
                if (RandomPoint(this.gameObject.transform.position, range, out point))
                {
                    navMeshAgent.SetDestination(point);
                    Debug.DrawRay(point, Vector3.up, Color.red, 10.0f);
                }
            }

            if ((Mathf.Approximately(gameObject.transform.position.x, point.x) || timer > 20f) && !trackingOwner)
            {
                arrived = true;
                timer = 0;
            }

            yield return new WaitForSeconds(0.25f);
        }
    }

    void TrackingOwner()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            trackingOwner = true;
        }

        if (trackingOwner)
        {
            timer = 0;
            arrived = false;
            trackingOwner = true;

            point = owner.transform.position;
            navMeshAgent.SetDestination(owner.transform.position);
        }
    }

    void DogAnimation()
    {

            dogAnimator.SetBool("meetOwner", meetOwner);
            dogAnimator.SetBool("walking", walking);


    }


}
        /*
        else
        {
            navMeshAgent.isStopped = true;
            Collider[] colliders = Physics.OverlapSphere(transform.position, 20f);

            for (int i=0; i < colliders.Length; i++)
            {
                GameObject gameObject = colliders[i].GetComponent<GameObject>();


                if (gameObject.CompareTag("Owner"))
                {
                    owner = gameObject;
                    break; 
                }
            }

        }
        */

