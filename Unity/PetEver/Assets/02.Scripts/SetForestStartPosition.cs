using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetForestStartPosition : MonoBehaviour
{

    GameObject manCharacter;

    // Start is called before the first frame update
    void Start()
    {
        manCharacter = GameObject.FindGameObjectWithTag("Owner");
         
        //Set the Character's initial position and rotation
        manCharacter.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
