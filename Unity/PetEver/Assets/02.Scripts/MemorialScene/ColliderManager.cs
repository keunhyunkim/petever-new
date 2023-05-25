using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    private JoyStickBtnManager joyStickBtnManager;

    [SerializeField] GameObject memorialSpaceScript;
    void Start()
    {
        joyStickBtnManager = memorialSpaceScript.GetComponent<JoyStickBtnManager>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Owner")
        {
            if (this.gameObject.name == "Wall")
            {
                joyStickBtnManager.showWallIcons();
            }
            else if (this.gameObject.tag == "Frame")
            {
                joyStickBtnManager.showPhotoIcons();
            }
            else if (this.gameObject.name == "MemorialCube")
            {
                joyStickBtnManager.showTreeIcons();
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Owner")
        {
            if (joyStickBtnManager.Buttons.Count > 0)
            {
                joyStickBtnManager.deleteIcons();
            }
        }
    }
}
