using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GoBackWorldScenefromMemorialScene : MonoBehaviour
{
    GameObject ManCharacter;
    GameObject MainCanvas;
    GameObject MainEvent;
    GameObject noticeText;

    private bool isEntered = false;
    [SerializeField] RectTransform fader;
    void Start()
    {
        //ManCharacter = GameObject.FindGameObjectWithTag("Owner");
        //MainCanvas = GameObject.FindGameObjectWithTag("UICanvas");
        //MainEvent = GameObject.FindGameObjectWithTag("MainEventSystem");
        fader = GameObject.Find("MemorialSceneCanvas").transform.GetChild(1).GetComponent<RectTransform>();
        noticeText = GameObject.FindGameObjectWithTag("NoticeText");
    }

    IEnumerator<object> GoWorldScene()
    {
        ManCharacter = GameObject.FindGameObjectWithTag("Owner");
        MainCanvas = GameObject.FindGameObjectWithTag("UICanvas");
        MainEvent = GameObject.FindGameObjectWithTag("MainEventSystem");

        ManCharacter.transform.position = new Vector3(-28.7f, 0f, -38.5f);
        string sceneName = "WorldScene";

        TextMeshProUGUI mText = noticeText.GetComponent<TextMeshProUGUI>();
        if (mText != null)
        {
            {
                mText.text = "지금 탄이의 추모식이 열리고 있어요";
            }
        }

        Scene currentScene = SceneManager.GetActiveScene();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.MoveGameObjectToScene(ManCharacter, SceneManager.GetSceneByName(sceneName));
        SceneManager.MoveGameObjectToScene(MainEvent, SceneManager.GetSceneByName(sceneName));
        SceneManager.MoveGameObjectToScene(MainCanvas, SceneManager.GetSceneByName(sceneName));
        SceneManager.UnloadSceneAsync(currentScene);


    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Owner")
        {
            if (isEntered == false)
            {
                fader.gameObject.SetActive(true);
                LeanTween.scale(fader, new Vector3(30, 30, 30), 0f);
                LeanTween.scale(fader, new Vector3(1, 1, 1), 1.0f).setOnComplete(() =>
                {
                    StartCoroutine(GoWorldScene());
                });
                isEntered = true;
            }
        }
    }
}
