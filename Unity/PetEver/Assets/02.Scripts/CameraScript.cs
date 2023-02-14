using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;


public class CameraScript : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    public GameObject tPlayer;
    public Transform tFollowTarget;
    private Scene nowScene;

    // Start is called before the first frame update
    void Start()
    {
        nowScene = SceneManager.GetActiveScene();
        vcam = GetComponent<CinemachineVirtualCamera>();
        tPlayer = null;

        if (tPlayer == null){
            tPlayer = GameObject.FindGameObjectWithTag("Owner");
            if (tPlayer != null){
                tFollowTarget = tPlayer.transform;
                vcam.Follow = tFollowTarget;

                if (nowScene.name == "MemorialScene")
                    vcam.LookAt = tFollowTarget;

                else 
                    vcam.LookAt = tFollowTarget;
            }
        }      


    }

    // Update is called once per frame
    void Update()
    {

    }
}
