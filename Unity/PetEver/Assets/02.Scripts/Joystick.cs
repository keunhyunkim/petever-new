using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


// ��ó : https://scvtwo.tistory.com/111
public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    RectTransform m_rectBack;
    RectTransform m_rectJoystick;

    Transform m_trMan;
    float m_fRadius;
    float m_fSpeed = 8.0f; // �̵��ӵ� ����
    float m_fSqr = 0f; //sqareroot(2D �� ������ �Ÿ�-���̽�ƽ ������ �Ÿ�)

    Vector3 m_vecMove;

    Vector2 m_vecNormal;

    bool m_bTouch = false;


    void Start()
    {
        m_rectBack = transform.Find("JoystickBGD").GetComponent<RectTransform>();
        m_rectJoystick = transform.Find("JoystickBGD/JoystickPad").GetComponent<RectTransform>();

        m_trMan = GameObject.Find("Man").transform;

        // JoystickBackground�� �������Դϴ�.
        m_fRadius = m_rectBack.rect.width * 0.5f;
    }

    void Update()
    {
        if (m_bTouch)
        {
            m_trMan.position += m_vecMove;
        }

    }

    void OnTouch(Vector2 vecTouch)
    {
        Vector2 vec = new Vector2(vecTouch.x - m_rectBack.position.x, vecTouch.y - m_rectBack.position.y);


        // vec���� m_fRadius �̻��� ���� �ʵ��� �մϴ�.
        vec = Vector2.ClampMagnitude(vec, m_fRadius);
        m_rectJoystick.localPosition = vec;

        // ���̽�ƽ ���� ���̽�ƽ���� �Ÿ� ������ �̵��մϴ�.
        float fSqr = (m_rectBack.position - m_rectJoystick.position).sqrMagnitude / (m_fRadius * m_fRadius);

        // ��ġ��ġ ����ȭ
        Vector2 vecNormal = vec.normalized;

        m_vecMove = new Vector3(vecNormal.x * m_fSpeed * Time.deltaTime * fSqr, 0f, vecNormal.y * m_fSpeed * Time.deltaTime * fSqr);
        m_trMan.eulerAngles = new Vector3(0f, Mathf.Atan2(vecNormal.x, vecNormal.y) * Mathf.Rad2Deg, 0f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnTouch(eventData.position);
        m_bTouch = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnTouch(eventData.position);
        m_bTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // ���� ��ġ�� �ǵ����ϴ�.
        m_rectJoystick.localPosition = Vector2.zero;
        m_bTouch = false;
    }
}

