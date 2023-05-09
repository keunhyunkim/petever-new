using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchZoom : MonoBehaviour
{
    public Touch touch0;
    public Touch touch1;
 
    private float distance;
 
    private float newDistance0;
    private float newDistance1;
 
    private float newDistance;
 
    public float scaleSpeed;
 
    public float scaleValue;    
    public float pinchZoom = 1.5f;

 
    private Vector2 midpoint;
 
    private bool firstRun;
 
    private void Start()
    {
        distance = 1;
        newDistance = 1;
    }
 
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touch0 = Input.GetTouch(0);
            touch1 = Input.GetTouch(1);
 
            midpoint = (touch0.position + touch1.position) / 2;
 
            distance = Vector2.Distance(midpoint, (touch0.position + touch1.position) / 2);
        }
        if (Input.touchCount == 2)
        {
 
            touch0 = Input.GetTouch(0);
            touch1 = Input.GetTouch(1);
 
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {

                firstRun = false;
                distance = Vector2.Distance(midpoint, (touch0.position + touch1.position) / 2);
            }
 
            if (touch0.phase == TouchPhase.Moved && touch1.phase == TouchPhase.Moved)
            {
                if (firstRun)
                {
                    distance = newDistance;
                }
                firstRun = true;
 
                midpoint = (touch0.position + touch1.position) / 2;
                newDistance0 = Vector2.Distance(midpoint, touch0.position);
                newDistance1 = Vector2.Distance(midpoint, touch1.position);
                newDistance = (newDistance0 + newDistance1) / 2;
            }
 

            scaleValue = (scaleSpeed * (newDistance - distance));  //Must move on slope scaleSpeed *   + 0.1f

 
            if ((gameObject.transform.localScale.x > 10 && scaleValue < 0) || (gameObject.transform.localScale.x < 0.1f && scaleValue > 0) || (gameObject.transform.localScale.x < 10000 && gameObject.transform.localScale.x > 0.1f))
            {
                 gameObject.transform.localScale = Vector3.one * pinchZoom;
            }
        }
    }
}