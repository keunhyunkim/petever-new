using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class LoadSceneManager : MonoBehaviour
{
    public static LoadSceneManager Instance;

    [SerializeField] private GameObject loaderCanvas;
    [SerializeField] private Image progressBar;
    [SerializeField] private GameObject mainText;
    [SerializeField] private GameObject progressText;
    [SerializeField] private GameObject dogModel;
    private TextMeshProUGUI mainTextTmp;
    private TextMeshProUGUI progressTextTmp;
    private string statusText;
    private int animFlag = 5;
    private Animator dogAnimator;
    private float _target;


    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        mainTextTmp = mainText.GetComponent<TextMeshProUGUI>();
        progressTextTmp = progressText.GetComponent<TextMeshProUGUI>();
        dogAnimator = dogModel.GetComponent<Animator>();

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

    public async void LoadScene(string sceneName)
    {

        _target = 0;
        progressBar.fillAmount = 0;
        setStatusText();
        setTitleText();
        loaderCanvas.SetActive(true);
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        dogAnimator.SetInteger("animFlag", animFlag);

        do
        {
            await Task.Delay(100);
            _target = scene.progress;
        } while (scene.progress < 0.90f);

        scene.allowSceneActivation = true;
        loaderCanvas.SetActive(false);
        dogAnimator.SetInteger("animFlag", 0);
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
}
