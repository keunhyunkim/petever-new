using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHandler : MonoBehaviour
{

    [SerializeField] RectTransform fader;

    // Start is called before the first frame update
    void Start()
    {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0f);
        LeanTween.scale(fader, new Vector3(30, 30, 30), 1.0f).setOnComplete(() =>
        {
            fader.gameObject.SetActive(false);
        });
    }



}
