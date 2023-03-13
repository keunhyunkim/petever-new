using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogEscort : MonoBehaviour
{
    private NavMeshAgent navMeshAgent; 
    public static bool welcomeEscort;
    public static bool waitOwner;
    private Vector3[] escortPoint; 
    private GameObject owner, emotionBubble, questionMark;
    private float dis_Owner2Dog; // distance between owner and dog
    private float dis_Owner2Point; // distance between owner and point
    private float dis_Dog2Point; // distance between dog and point
   
    void Awake()
    {
        welcomeEscort = false;
        waitOwner = false;
        dis_Owner2Point = 10f;

        escortPoint = new Vector3[3]; 
        escortPoint[0] = GameObject.Find("checkPoint1").GetComponent<Transform>().position;
        escortPoint[1] = GameObject.Find("checkPoint2").GetComponent<Transform>().position;
        escortPoint[2] = GameObject.Find("checkPoint3").GetComponent<Transform>().position;
        //owner = GameObject.FindGameObjectWithTag("Owner");


    }

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        owner = GameObject.FindGameObjectWithTag("Owner");
        emotionBubble = GameObject.Find("emotionBubble");
        questionMark = GameObject.Find("questionMark");

        StartCoroutine(DogEscorting());

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(dis_Owner2Point);        
        // Debug.Log(welcomeEscort);

        dis_Owner2Dog = Vector3.Distance(owner.transform.position, gameObject.transform.position);
        dis_Owner2Point = Vector3.Distance(owner.transform.position, escortPoint[0]);
        dis_Dog2Point = Vector3.Distance(gameObject.transform.position, escortPoint[0]);
    }


    IEnumerator DogEscorting()
    {
        emotionBubble.SetActive(false);
        questionMark.SetActive(false);


        while(true)
        {
            if (welcomeEscort)
            {
                if (dis_Owner2Dog > 12f)
                {
                    waitOwner = true;
                    navMeshAgent.enabled = false;

                    emotionBubble.SetActive(true);
                    questionMark.SetActive(true);

                    LookOwner();
                    //gameObject.transform.LookAt(owner.transform);
                }

                else
                {
                    waitOwner = false;
                    navMeshAgent.SetDestination(escortPoint[0]);

                    emotionBubble.SetActive(false);
                    questionMark.SetActive(false);

                    navMeshAgent.enabled = true;   
                }


                if (dis_Owner2Point < 2f)
                {
                    welcomeEscort = false;
                }
            }
            yield return new WaitForSeconds(0.25f);
        }


    }

    void LookOwner()
    {
        if (owner != null)
        {
            Vector3 dir = (owner.transform.position-gameObject.transform.position);
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.LookRotation(dir),Time.deltaTime*50f);
        }
    }

}
