using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingPoint : MonoBehaviour
{

    GameObject Player;
    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Owner");
        Player.transform.position = gameObject.transform.position + new Vector3(0f, -2.23f, 0f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
