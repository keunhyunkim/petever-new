using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
 
public class DogInformation : MonoBehaviour
{
    Vector3 m_vecMouseDownPos;
    public GameObject dogInfoCG;
    GameObject dogInfoCanvas;

    void Start()
    {
        dogInfoCanvas = GameObject.FindGameObjectWithTag("DogInfo");
        if (dogInfoCanvas == null)
        {
            GameObject canvas = Instantiate(dogInfoCG) as GameObject;
        }

        controlDogInfoCG(false, null);
    }

    private void controlDogInfoCG(bool showflag, Collider dog)
    {
        dogInfoCG = GameObject.FindGameObjectWithTag("DogInfo");
        if (showflag == true) {
            dogInfoCG.GetComponent<Canvas>().GetComponent<CanvasGroup>().alpha = 1;
            GameObject targetDog = GameObject.Find(dog.name);
            dogInfoCG.transform.GetChild(4).GetComponent<Image>().sprite = targetDog.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite;
        } else {
            dogInfoCG.GetComponent<Canvas>().GetComponent<CanvasGroup>().alpha = 0;
        }
        dogInfoCG.GetComponent<Canvas>().GetComponent<CanvasGroup>().interactable = showflag;
        dogInfoCG.GetComponent<Canvas>().GetComponent<CanvasGroup>().blocksRaycasts = showflag;
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
            // Prevent not to click another object
            if (EventSystem.current.IsPointerOverGameObject())
            return;

            // 카메라에서 스크린에 마우스 클릭 위치를 통과하는 광선을 반환합니다.
            Ray ray = Camera.main.ScreenPointToRay(m_vecMouseDownPos);
            RaycastHit hit;
 
            // 광선으로 충돌된 collider를 hit에 넣습니다.
            if(Physics.Raycast(ray, out hit))
            {
                // 어떤 오브젝트인지 로그를 찍습니다.
                if (hit.collider.tag == "NPC" || hit.collider.tag == "OwnerDog") {
                    controlDogInfoCG(true, hit.collider);
                }
                
            }
 
        }
    }

    public void OnDogInfoXClicked()
    {
        controlDogInfoCG(false, null);
    }
}
