using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeWalkButton : MonoBehaviour
{

    public Animator takeWalk;  

    // Start is called before the first frame update
    void Start()
    {
        takeWalk = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClicktakeWalk()
    {
        takeWalk.Play("metarig|feetup_2");
    }
}
