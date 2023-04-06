using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
 
public class DogInformation : MonoBehaviour
{
    Vector3 m_vecMouseDownPos;
    public GameObject dogInfoCG;
    static GameObject dogInfoCanvas;
    static GameObject icanvas;
    private string[] dogInfoText;

    void Start()
    {
        dogInfoCanvas = GameObject.FindGameObjectWithTag("DogInfo");
        if (dogInfoCanvas == null)
        {
            icanvas = Instantiate(dogInfoCG) as GameObject;
        }
        controlDogInfoCG(false, null);
    }

    private void parseDogInfo(string info)
    {
        dogInfoText = info.Split(',');
    }

    private GameObject GetChildWithName(GameObject obj, string childName)
    {
        return obj.transform.Find(childName)?.gameObject;
    }

    private void controlDogInfoCG(bool showflag, Collider dog)
    {
        dogInfoCG = icanvas;
        if (showflag == true) {
            dogInfoCG.GetComponent<Canvas>().GetComponent<CanvasGroup>().alpha = 1;
            GameObject targetDog = GameObject.Find(dog.name);
            GetChildWithName(dogInfoCG, "DogImage").GetComponent<Image>().sprite = GetChildWithName(targetDog, "Info/DogImage").GetComponent<Image>().sprite;
            
            parseDogInfo(targetDog.transform.GetComponent<Text>().text);
            string msg = dogInfoText[1];
            msg = msg.Replace("\\n", "\n").Replace("\\", "");

            GetChildWithName(dogInfoCG, "PopupTitle").GetComponent<TextMeshProUGUI>().text = dogInfoText[0];
            GetChildWithName(dogInfoCG, "PopupDetail").GetComponent<TextMeshProUGUI>().text = msg;
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
                if (hit.collider.tag == "DogNPC" || hit.collider.tag == "OwnerDog") {
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
