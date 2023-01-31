using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //namespace for using UI system
//using UnityEngine.Events; //namespace for using Event system API
using UnityEngine.EventSystems; 


public class MemorialUIManager: MonoBehaviour
{
    public Button weatherBtn_1, weatherBtn_2, weatherBtn_3, weatherBtn_4;
    public Button optionBtn_subject, optionBtn_text, optionBtn_picture, optionBtn_sticker;
   // public UnityAction action;

    private Sprite WeatherBtn_select;
    private Sprite WeatherBtn_nonselect;

    void Awake()
    {
      //  WeatherBtn_select = Resources.Load<Sprite>("WeatherBtn_select");
       // WeatherBtn_nonselect = Resources.Load<Sprite>("WeatherBtn_nonselect");
    }

    // Start is called before the first frame update
    void Start()
    {
        WeatherBtn_select = Resources.Load<Sprite>("WeatherBtn_select");
        WeatherBtn_nonselect = Resources.Load<Sprite>("WeatherBtn_nonselect");

        weatherBtn_1.onClick.AddListener(delegate {ChangeImage();});
        weatherBtn_2.onClick.AddListener(delegate {ChangeImage();});
        weatherBtn_3.onClick.AddListener(delegate {ChangeImage();});
        weatherBtn_4.onClick.AddListener(delegate {ChangeImage();});
    }

    public void ChangeImage()
    {
        GameObject clickButton = EventSystem.current.currentSelectedGameObject;

        if (clickButton.GetComponent<Image>().sprite == WeatherBtn_select)
        {
            clickButton.GetComponent<Image>().sprite = WeatherBtn_nonselect;
        }

        else if(clickButton.GetComponent<Image>().sprite == WeatherBtn_nonselect)
        {
            clickButton.GetComponent<Image>().sprite = WeatherBtn_select;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
