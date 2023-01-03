using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


// reference: https://scvtwo.tistory.com/111
public class PlayerController : MonoBehaviour
{
    float playerSpeed = 5.0f; // Player character speed
    private PlayerInput inputValue;
    private Animator playerAnimator;


    Vector3 move;

    GameObject Player; 
    

    void Start()
    {
        Player = GameObject.Find("Man");
        inputValue = GameObject.Find("JoystickBGD").GetComponent<PlayerInput>(); // get joystick input data from 'PlayerInput' class 
        playerAnimator = GetComponent<Animator>();

    }

    void FixedUpdate()
    {
        move = new Vector3(inputValue.joystick_x * playerSpeed * Time.deltaTime, 0f, inputValue.joystick_y * playerSpeed * Time.deltaTime);
        Player.transform.eulerAngles = new Vector3(0f, Mathf.Atan2(-inputValue.joystick_x, -inputValue.joystick_y) * Mathf.Rad2Deg, 0f);

        if (inputValue.lastpos != (Vector3.one)*99999)
        {
            Player.transform.eulerAngles = new Vector3(0f, Mathf.Atan2(-inputValue.lastpos.x, -inputValue.lastpos.y) * Mathf.Rad2Deg, 0f);
        }

        Player.transform.position += move;
        playerAnimator.SetFloat("walking", move.magnitude);
    }


}

