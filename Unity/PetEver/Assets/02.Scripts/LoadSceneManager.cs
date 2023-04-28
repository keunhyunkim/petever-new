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
    [SerializeField] private GameObject progressText;
    [SerializeField] private GameObject dogModel;
    private TextMeshProUGUI mText;
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

        mText = progressText.GetComponent<TextMeshProUGUI>();
        dogAnimator = dogModel.GetComponent<Animator>();
    }

    public async void LoadScene(string sceneName)
    {

        _target = 0;
        progressBar.fillAmount = 0;
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
        if (mText != null)
        {
            mText.text = "(" + targetToInt.ToString("##") + "%)";
        }
    }
}
