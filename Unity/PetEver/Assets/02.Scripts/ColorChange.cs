using UnityEngine;

public class ColorChange : MonoBehaviour
{
    Renderer capsuleColor;

    void Start()
    {
        capsuleColor = gameObject.GetComponent<Renderer>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (capsuleColor.material.color != Color.blue)
            {
                capsuleColor.material.color = Color.blue;
            }
            else
            {
                capsuleColor.material.color = Color.red;
            }
        }
    }
}