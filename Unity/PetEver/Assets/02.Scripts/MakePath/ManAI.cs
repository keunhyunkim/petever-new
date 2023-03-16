using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // add AI navigation system
using UnityEngine.EventSystems;
using System;


public class ManAI : MonoBehaviour
{

    private NavMeshAgent navMeshAgent; // assign navmeshagent component
    private Animator manAnimator;

    private bool arrived = true;
    private float range = 20f; // standard range for generating random point  
    private Vector3 point; // random point for man AI moving 
    private Vector3 lastpos; // for determine man is walking or not  
    private float timer = 0f;
    private float man_normalSpeed = 3.0f; 

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

    //make random point
    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range; // take random point from 'range' radious sphere around center
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


    }

    // Start is called before the first frame update
    void Start()
    {
        manAnimator = GetComponent<Animator>();
        //manAnimator.SetFloat("cycleOffset", Random.Range(0f,1/6f));

        navMeshAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(UpdatePath());
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(walking);
        timer += Time.deltaTime;
        ManAnimation();

        lastpos = gameObject.transform.position;
    }

    // randomly man moves 
    IEnumerator UpdatePath()
    {
        while (true)
        {
            if (gameObject.CompareTag("NPC"))
            {
                if (arrived)
                {
                    arrived = false;
                    if (RandomPoint(this.gameObject.transform.position, range, out point))
                    {
                        navMeshAgent.speed = man_normalSpeed;
                        navMeshAgent.SetDestination(point);
                        //Debug.DrawRay(point, Vector3.up, Color.red, 10.0f);
                    }
                }

                if (Mathf.Approximately(gameObject.transform.position.x, point.x) || timer > 4f)
                {
                    arrived = true;
                    timer = 0;
                }
            }

            yield return new WaitForSeconds(0.25f);
        }
    }


    void ManAnimation()
    {
        manAnimator.SetBool("walking", walking);
    }

}

