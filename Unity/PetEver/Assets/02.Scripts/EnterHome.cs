using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class EnterHome : MonoBehaviour
{
    

    private bool isEntered = false;
    private Vector3 m_currentDirection = Vector3.zero;

   
    private void Awake()
    {
      
    }

    private void OnCollisionEnter(Collision collision)
    {
       if (isEntered == false) {      
        /*
        * LoadSceneMode.Single  : 모든 씬을 닫고, 새로운 씬을 불러온다. 
        *  
        * LoadSceneMode.Additive : 현재 씬에 추가적으로 씬을 불러온다.
        */
        Debug.Log("New Scene Entered!!!");
        SceneManager.LoadScene("newScene", LoadSceneMode.Single); 
        //SceneManager.LoadScene("SceneName", LoadSceneMode.Additive);
       }
    }

    private void OnCollisionStay(Collision collision)
    {
       
    }

    private void OnCollisionExit(Collision collision)
    {
       
    }

    private void Update()
    {
       
    }

}
