using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DimImageButton : MonoBehaviour
{

    public GameObject dimImageObject;
    public Sprite voiceBtnOriginSprite;
    public GameObject voiceBtnImageObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickDimImage()
    {
        dimImageObject.SetActive(false);
        voiceBtnImageObject.GetComponent<Image>().sprite = voiceBtnOriginSprite;
    }
}
