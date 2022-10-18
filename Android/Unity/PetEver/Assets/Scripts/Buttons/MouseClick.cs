using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseClick : MonoBehaviour
{
    public Animator animator;
    public GameObject character;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

  
    public void OnClickCharacter()
    {
        Debug.Log("OnClickCharacter");
        animator.GetComponent<Animator>().Play("Base Layer.feedup");
    }

}
