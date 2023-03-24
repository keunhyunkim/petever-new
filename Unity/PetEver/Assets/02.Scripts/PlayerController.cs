using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


// reference: https://scvtwo.tistory.com/111
public class PlayerController : MonoBehaviour
{
    private PlayerInput inputValue;
    public static Animator playerAnimator;

    public static float playerSpeed = 8.0f; // Player character speed
    public static bool isForest;
    public static float mov_x;
    public static float mov_y;

    Vector3 move;

    GameObject Player;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Owner");
        inputValue = GameObject.Find("JoystickBGD").GetComponent<PlayerInput>(); // get joystick input data from 'PlayerInput' class 
        playerAnimator = GetComponent<Animator>();

    }

    void FixedUpdate()
    {

        if (inputValue == null)
        {
            return;
        }
        // Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        // Player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        if (isForest != true)
        {
            mov_x = inputValue.joystick_x;
            mov_y = inputValue.joystick_y;
        }

        move = new Vector3(mov_x * playerSpeed * Time.deltaTime, 0f, mov_y * playerSpeed * Time.deltaTime);
        Player.transform.eulerAngles = new Vector3(0f, Mathf.Atan2(mov_x, mov_y) * Mathf.Rad2Deg, 0f);

        if (inputValue.lastpos != (Vector3.one) * 99999)
        {
            Player.transform.eulerAngles = new Vector3(0f, Mathf.Atan2(inputValue.lastpos.x, inputValue.lastpos.y) * Mathf.Rad2Deg, 0f);
        }

        Player.transform.position += move;
        playerAnimator.SetFloat("walkingSpeed", move.magnitude);
    }
}

