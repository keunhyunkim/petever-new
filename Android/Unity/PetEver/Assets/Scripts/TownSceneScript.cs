using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownSceneScript : MonoBehaviour
{
    public GameObject StatusBarImage;
    public GameObject StatusText;

    // Start is called before the first frame update
    void Start()
    {
        ShowImage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator HideAfterSec(float delay)
    {
        StatusBarImage.SetActive(true);
        yield return new WaitForSeconds(delay);
        StatusBarImage.SetActive(false);
    }

    public void ShowImage()
    {
        StatusBarImage.SetActive(true);
    }

    public void HideImage()
    {
        StartCoroutine(HideAfterSec(2.0f));
    }
}
