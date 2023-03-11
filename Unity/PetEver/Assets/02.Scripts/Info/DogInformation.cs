using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class DogInformation : MonoBehaviour
{
    Vector3 m_vecMouseDownPos;
    public GameObject dogInfoCG;

    void Start()
    {
    }
 
    void Update()
    {
#if UNITY_EDITOR
        // 마우스 클릭 시
        if (Input.GetMouseButtonDown(0))
#else
        // 터치 시
        if (Input.touchCount > 0)
#endif
        {
#if UNITY_EDITOR
            m_vecMouseDownPos = Input.mousePosition;
#else
            m_vecMouseDownPos = Input.GetTouch(0).position;
            if(Input.GetTouch(0).phase != TouchPhase.Began)
                return;
#endif
            // 카메라에서 스크린에 마우스 클릭 위치를 통과하는 광선을 반환합니다.
            Ray ray = Camera.main.ScreenPointToRay(m_vecMouseDownPos);
            RaycastHit hit;
 
            // 광선으로 충돌된 collider를 hit에 넣습니다.
            if(Physics.Raycast(ray, out hit))
            {
                // 어떤 오브젝트인지 로그를 찍습니다.
                Debug.Log(hit.collider.name);
                dogInfoCG.GetComponent<Canvas>().GetComponent<CanvasGroup>().alpha = 1;
                dogInfoCG.GetComponent<Canvas>().GetComponent<CanvasGroup>().interactable = true;
                dogInfoCG.GetComponent<Canvas>().GetComponent<CanvasGroup>().blocksRaycasts = true;

            }
 
        }
    }
}
