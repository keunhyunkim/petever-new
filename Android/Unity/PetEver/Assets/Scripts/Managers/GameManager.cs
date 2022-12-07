using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject StatusBarImage;
    public GameObject StatusText;
    private TextMeshProUGUI tmpText;

    private AndroidJavaObject activityContext = null;
    public GameObject maltesePrefab;
    public GameObject pomeLongPrefab;
    public GameObject pomeShortPrefab;

    public GameObject heartPrefab;

    public GameObject dogModel;
    public Animator anim;
    String breed = "";
    String petName = "";
    bool heartAnimate = false;
    GameObject heart;
    float offsetTime = 5.5f;
    private float timer = 0f;
    GameObject bone;
    public GameObject bonePrefab;
    bool boneCreate = false;
    void Awake()
    {
        Vector3 dogScale = new Vector3(0.8f, 0.8f, 0.8f);

        using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            breed = "";
            petName = "";
            GameObject dogPrefab;
            try
            {
                activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");

                AndroidJavaObject intent = activityContext.Call<AndroidJavaObject>("getIntent");
                breed = intent.Call<String>("getStringExtra", "breed");
                petName = intent.Call<String>("getStringExtra", "petname");

                Debug.Log("[intent data] arguments : " + breed);
                Debug.Log("[intent data] arguments : " + petName);
            }
            catch (Exception e)
            {
                Debug.Log("GameManager Exception : " + e.ToString());
            }

            switch (breed)
            {
                case "POME_LONG":
                    dogPrefab = pomeLongPrefab;
                    break;
                case "POME_SHORT":
                    dogPrefab = pomeShortPrefab;
                    break;
                case "MALTESE":
                    dogPrefab = maltesePrefab;
                    break;
                default:
                    dogPrefab = pomeShortPrefab;
                    break;
            }

            GameObject dog = Instantiate(dogPrefab, GameObject.Find("Object Parent").transform) as GameObject;
            dog.transform.localScale = dogScale;


        }
    }
    // Start is called before the first frame update
    void Start()
    {
        tmpText = StatusText.GetComponent<TextMeshProUGUI>();

        tmpText.text = petName + "는 기분이 좋아요!";

        dogModel = GameObject.FindGameObjectWithTag("Dog");
        anim = this.dogModel.GetComponent<Animator>();

    }

    IEnumerator HideAfterSec(float delay)
    {
        StatusBarImage.SetActive(true);
        tmpText.text = petName + "가 산책을 시작했어요!";
        yield return new WaitForSeconds(delay);
        StatusBarImage.SetActive(false);
    }

    public void ShowImage()
    {
        StatusBarImage.SetActive(true);
    }

    public void HideImage()
    {
        StartCoroutine(HideAfterSec(10.0f));
    }
    private float movementSpeed = 2f;
    // Update is called once per frame
    void Update()
    {

        if (boneCreate == true)
        {
            timer += Time.deltaTime;
            if (timer > offsetTime)
            {
                timer = 0f;
                bone.SetActive(false);
            }
        }

        if (heartAnimate == true)
        {
            if (heart.transform.position.y < 2)
            {
                heart.transform.position = heart.transform.position + new Vector3(0, 1 * movementSpeed * Time.deltaTime, 0);
            }
            else
            {
                heartAnimate = false;
                heart.SetActive(false);
            }
        }
    }

    //movement speed in units per second

    public void CreateHeart()
    {
        heart = Instantiate(heartPrefab, GameObject.Find("Hearts").transform) as GameObject;
        heartAnimate = true;
    }

    public void OnClicktakeWalk()
    {
        try
        {
            if (anim != null)
            {
                if (breed == "POME_SHORT")
                {
                    anim.Play("metarig|feetup_2");
                }
                else if (breed == "POME_LONG")
                {
                    anim.Play("metarig|tilting");
                }
                else
                {
                    anim.Play("metarig|tilting");
                }
                CreateHeart();
            }
            HideImage();
        }
        catch (Exception e)
        {
            Debug.Log("OnClicktakeWalk Exception : " + e.ToString());
        }
    }
    public void CreateBone()
    {
        bone = Instantiate(bonePrefab, GameObject.Find("Hearts").transform) as GameObject;
        boneCreate = true;
    }
    public void OnClickTreat()
    {
        try
        {
            CreateBone();
            if (anim != null)
            {
                anim.Play("metarig|idle_2_sniffing");
            }

        }
        catch (Exception e)
        {
            Debug.Log("OnClickTreat Exception : " + e.ToString());
        }
    }
    public void OnClickCheerUp()
    {
        try
        {
            if (anim != null)
            {
                anim.Play("metarig|tailing");
            }

        }
        catch (Exception e)
        {
            Debug.Log("OnClickCheerUp Exception : " + e.ToString());
        }
    }

    public void OnClickCharacter()
    {
        try
        {
            if (anim != null)
            {
                anim.Play("metarig|givehand");
            }

        }
        catch (Exception e)
        {
            Debug.Log("OnClickCharacter Exception : " + e.ToString());
        }
    }
}
