using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// https://gist.github.com/alialacan/1eddcd107f4a48a46dea17695ca151f2

public class SwipeDetector : MonoBehaviour
{
    private Vector2 fingerDownPos;
    private Vector2 fingerUpPos;
    public Animator anim;
    public GameObject dogModel;
    public bool detectSwipeAfterRelease = false;

    public float SWIPE_THRESHOLD = 20f;

    void Start()
    {
        dogModel = GameObject.FindGameObjectWithTag("Dog");
        anim = this.dogModel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUpPos = touch.position;
                fingerDownPos = touch.position;
            }

            //Detects Swipe while finger is still moving on screen

            if (touch.phase == TouchPhase.Moved)
            {
                if (!detectSwipeAfterRelease)
                {
                    fingerDownPos = touch.position;
                    DetectSwipe();
                }
            }

            /*
                //Detects swipe after finger is released from screen
                if (touch.phase == TouchPhase.Ended)
                {
                    fingerDownPos = touch.position;
                    DetectSwipe();
                }
                */
        }
    }

    void DetectSwipe()
    {
        if (HorizontalMoveValue() > SWIPE_THRESHOLD && HorizontalMoveValue() > VerticalMoveValue())
        {
            Debug.Log("Horizontal Swipe Detected!");
            if (anim != null)
            {
                anim.Play("metarig|heading");
            }

        }
    }

    float VerticalMoveValue()
    {
        return Mathf.Abs(fingerDownPos.y - fingerUpPos.y);
    }

    float HorizontalMoveValue()
    {
        return Mathf.Abs(fingerDownPos.x - fingerUpPos.x);
    }

    void OnSwipeLeft()
    {
        Debug.Log("left Swipe Detected!");
        //Do something when swiped left


    }


    void OnSwipeRight()
    {
        Debug.Log("right Swipe Detected!");
        //Do something when swiped right
        if (anim != null)
        {
            anim.Play("metarig|heading");
        }
    }
}