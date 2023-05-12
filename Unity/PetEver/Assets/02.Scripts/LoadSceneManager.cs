using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class LoadSceneManager : MonoBehaviour
{

    [SerializeField] private GameObject loaderCanvas;
    [SerializeField] private Image progressBar;
    [SerializeField] private GameObject mainText;
    [SerializeField] private GameObject progressText;
    private GameObject dogModel;
    private TextMeshProUGUI mainTextTmp;
    private TextMeshProUGUI progressTextTmp;
    private string statusText;
    private Animator dogAnimator;
    private float _target;


    // Start is called before the first frame update
    void Awake()
    {
        mainTextTmp = mainText.GetComponent<TextMeshProUGUI>();
        progressTextTmp = progressText.GetComponent<TextMeshProUGUI>();
        dogModel = GameObject.FindGameObjectWithTag("OwnerDog");
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    private void setTitleText()
    {
        string name = SceneManager.GetActiveScene().name;
        if ("MySpaceScene".Equals(name))
        {
            mainTextTmp.text = "무지개 우주로 떠나볼까요?";
        }
    }
    private void setStatusText()
    {
        int randNum = Random.Range(0, 3);
        switch (randNum)
        {
            case 0:
                statusText = "방방 뛰는 댕댕이 진정시키는 중... ";
                break;
            case 1:
                statusText = "무지개 우주 댕댕이들 집합 중... ";
                break;
            case 2:
                statusText = "댕댕이 간식 준비 중... ";
                break;
            default:
                statusText = "댕댕이 간식 준비 중... ";
                break;
        }
    }
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
    IEnumerator LoadSceneAsync(string sceneName)
    {

        yield return null;
        _target = 0;
        progressBar.fillAmount = 0;
        setStatusText();
        loaderCanvas.SetActive(true);
        if (dogModel == null)
        {
            dogModel = GameObject.FindGameObjectWithTag("OwnerDog");
        }

        dogAnimator = dogModel.GetComponent<Animator>();
        if (dogAnimator != null)
        {
            dogAnimator.SetBool("run", true);
        }

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {

            _target = asyncOperation.progress;
            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                if (dogAnimator != null)
                {
                    dogAnimator.SetBool("run", false);
                }
                //Activate the Scene
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
    private void ChangedActiveScene(Scene current, Scene next)
    {
        if (dogAnimator != null)
        {
            dogAnimator.SetBool("run", false);
        }
    }
    void Update()
    {
        progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, _target, 3 * Time.deltaTime);

        var targetToInt = _target * 100;
        if (progressTextTmp != null)
        {
            progressTextTmp.text = statusText + "(" + targetToInt.ToString("##") + "%)";
        }


    }

    void showCanvasGroup(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    void hideCanvasGroup(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
}
