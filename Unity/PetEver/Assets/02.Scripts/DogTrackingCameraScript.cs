using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;


public class DogTrackingCameraScript : MonoBehaviour
{
    private CinemachineVirtualCamera vcam, cineCam;
    private CinemachineTransposer cineTransposer;
    public GameObject tPlayer;
    private Vector3 offset; 




    // Start is called before the first frame update
    void Start()
    {

       tPlayer = GameObject.FindGameObjectWithTag("OwnerDog");
       //offset = gameObject.transform.position - tPlayer.transform.position;
       offset = new Vector3(0f,0.5f,0f);

    }

    // Update is called once per frame
    void LateUpdate()
    {
        gameObject.transform.position = tPlayer.transform.position + offset;

    }
}
