using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;


public class CameraScript : MonoBehaviour
{
    private CinemachineVirtualCamera vcam, cineCam;
    private CinemachineTransposer cineTransposer;
    public GameObject tPlayer;
    public Transform tFollowTarget;
    private Scene nowScene;




    // Start is called before the first frame update
    void Start()
    {
        cineCam = this.GetComponent<CinemachineVirtualCamera>();
        cineTransposer = cineCam.GetCinemachineComponent<CinemachineTransposer>();

        nowScene = SceneManager.GetActiveScene();
        vcam = GetComponent<CinemachineVirtualCamera>();
        tPlayer = null;

        if (tPlayer == null){
            tPlayer = GameObject.FindGameObjectWithTag("Owner");
            if (tPlayer != null){
                tFollowTarget = tPlayer.transform;
                vcam.Follow = tFollowTarget;
                vcam.LookAt = tFollowTarget;
/*
                if (nowScene.name == "MemorialScene")
                {
                    cineTransposer.m_FollowOffset = new Vector3(0f, 3f, 16.2f);
                }
*/                
            }
        }      


    }

    // Update is called once per frame
    void Update()
    {
/*

        nowScene = SceneManager.GetActiveScene();
        if (nowScene.name == "MemorialScene")
        {
            cineTransposer.m_FollowOffset = new Vector3(0f, 3f, 16.2f);
        }
*/                

    }
}
