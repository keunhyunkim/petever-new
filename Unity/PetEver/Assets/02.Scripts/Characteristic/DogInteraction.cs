using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogInteraction : MonoBehaviour
{
    public GameObject DogCamera;

    // Start is called before the first frame update
    void Start()
    {
        DogCamera = GameObject.Find("Camera");
    }

    // Update is called once per frame
    // https://ssscool.tistory.com/336
    void Update()
    {
        if (Input.touchCount > 0)
        {
            // 싱글 터치.
            Touch touch = Input.GetTouch(0);
            Ray ray;
            RaycastHit hit;
 
            switch (touch.phase)
            {
                case TouchPhase.Began: // when touch starts
                    Debug.Log(touch.position.x);
                    Debug.Log(touch.position.y);

                    Vector3 touchPosToVector3 = new Vector3(touch.position.x,touch.position.y,-900);
                    //touchPos = DogCamera.GetComponent<Camera>().ScreenToWorldPoint(touchPosToVector3);
                    ray = DogCamera.GetComponent<Camera>().ScreenPointToRay(touchPosToVector3); 
                    if (Physics.Raycast(ray,out hit))
                    {
                        Debug.DrawLine(ray.origin, hit.point, Color.red, 10.5f);
                        Debug.Log("ok"); 
                        if(hit.collider.gameObject.name == "footTouch_R")
                        {
                            Debug.Log("R");
                        }
                        else if(hit.collider.gameObject.name == "footTouch_L")
                        {
                            Debug.Log("L");
                        }
                    }
                    else
                    {                        
                        Debug.DrawLine(ray.origin, hit.point, Color.blue, 10.5f);
                        Debug.Log("fail");
                    }
 
                    break;
                case TouchPhase.Moved:
                    // 터치 이동 시.
                    break;
 
                case TouchPhase.Stationary:
                    // 터치 고정 시.
                    break;
 
                case TouchPhase.Ended:
                    // 터치 종료 시. ( 손을 뗐을 시 )
                    break;
 
                case TouchPhase.Canceled:
                    // 터치 취소 시. ( 시스템에 의해서 터치가 취소된 경우 (ex: 전화가 왔을 경우 등) )
                    break;
            }
        }
    }
}
