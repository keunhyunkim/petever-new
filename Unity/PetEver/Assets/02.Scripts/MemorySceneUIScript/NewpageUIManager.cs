using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.EventSystems;

// Reference : https://greenapple16.tistory.com/275
//             https://www.youtube.com/watch?v=H2TpEq0Hr2g

public class NewpageUIManager : MonoBehaviour
{
    public RawImage rawImage; // take image RawImage
    public Button ImageBtn, StickerBtn, TextBtn, CompleteBtn, XBtn;
    public Toggle UploadToggle;
    public static GameObject UserInputTextBundle, StickerInventory;
    private Vector3 createPoint, stickercreatePoint;

    // for screen capture value
    public string folderName = "ScreenShots";
    public string fileName = "MyScreenShot";
    public string extName = "png";
    private string RootPath
        {
            get
            {
            //if UNITY_EDITOR || UNITY_STANDALONE 
            return Application.persistentDataPath;
            //elif UNITY_ANDROID
            //return $"/storage/emulated/0/DCIM/{Application.productName}/";
            //return Application.persistentDataPath;
            }
        }
    private string FolderPath => $"{RootPath}/{folderName}";
    private string TotalPath => $"{FolderPath}/{fileName}_{DateTime.Now.ToString("MMdd_HHmmss")}.{extName}";
    


    // Start is called before the first frame update
    void Start()
    {
        XBtn = GameObject.Find("XBtn").GetComponent<Button>();
        ImageBtn = GameObject.Find("ImageBtn").GetComponent<Button>();
        TextBtn = GameObject.Find("TextBtn").GetComponent<Button>();
        StickerBtn = GameObject.Find("StickerBtn").GetComponent<Button>();
        CompleteBtn = GameObject.Find("CompleteBtn").GetComponent<Button>();
        createPoint = GameObject.Find("MemorialSceneCanvas").GetComponent<RectTransform>().anchoredPosition;


        UserInputTextBundle = Resources.Load<GameObject>("Prefabs/UserInputTextBundle");
        StickerInventory = Resources.Load<GameObject>("Prefabs/StickerInventory");

        ImageBtn.onClick.AddListener(delegate { GetImage(); });
        TextBtn.onClick.AddListener(delegate { GetText(); });
        StickerBtn.onClick.AddListener(delegate { GetSticker(); });
        XBtn.onClick.AddListener(delegate { CloseUI(); });
        CompleteBtn.onClick.AddListener(delegate { SaveUI(); });

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveUI()
    {
        StartCoroutine(TakeScreenShotRoutine());
    }

    private IEnumerator TakeScreenShotRoutine()
    { 
            yield return new WaitForEndOfFrame();


            string totalPath = TotalPath;
            Texture2D screenTex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

            Rect area = new Rect(0f, 0f, Screen.width, Screen.height);
            screenTex.ReadPixels(area, 0, 0);


            // if folder doesn't exist, make new folder
            if (Directory.Exists(FolderPath) == false)
            {
                Directory.CreateDirectory(FolderPath);
            }

            // save screenshot
            File.WriteAllBytes(FolderPath, screenTex.EncodeToPNG());

            Destroy(screenTex);
    }

    public void CloseUI()
    {
        Destroy(gameObject);
    }

    public void GetText()
    {   
        UserInputTextBundle = Resources.Load<GameObject>("Prefabs/UserInputTextBundle");
        UserInputTextBundle = Instantiate(UserInputTextBundle, createPoint, Quaternion.identity, GameObject.Find("MemorialSceneCanvas").GetComponent<RectTransform>());
    }

    public void GetSticker()
    {
        stickercreatePoint = createPoint + new Vector3(0f, 468f, 0f);
        StickerInventory = Resources.Load<GameObject>("Prefabs/StickerInventory");
        StickerInventory = Instantiate(StickerInventory, stickercreatePoint, Quaternion.identity, GameObject.Find("MemorialSceneCanvas").GetComponent<RectTransform>());
    }

    public void GetImage()
    {
        NativeGallery.GetImageFromGallery((image) =>  //mobile gallery folder open using NativeGallery Plugin
        {
            FileInfo selectedImage = new FileInfo(image); //choose image from gallery folder

        /* set a limit on volume of picture
        if (selectedImage.Length > 50000000)  
        {
            return;
        }
        */
            if (!string.IsNullOrEmpty(image)) // if image is selected, start coroutine(load image)
            StartCoroutine(LoadImage(image));

        });
    }

    //image load coroutine            
    IEnumerator LoadImage(string imagePath)
    {
        byte[] imageData = File.ReadAllBytes(imagePath); // read file and put in byte array
        string imageName = Path.GetFileName(imagePath).Split('.')[0]; // save image name except image extension
        string saveImagePath = Application.persistentDataPath + "/Image"; // save data path in image folder 
                                                                          // for the first time, get image from gallery and next, get from folder

        if (Directory.Exists(saveImagePath)) // if file to save image is not exist, make path first
        {
            Directory.CreateDirectory(saveImagePath);
        }

        File.WriteAllBytes(saveImagePath + imageName + ".jpg", imageData); // set path and file name to save image

        var tempImage = File.ReadAllBytes(imagePath);

        Texture2D texture = new Texture2D(1080, 1440);
        texture.LoadImage(tempImage); // transfer byte array to texture 2D

        rawImage.texture = texture;

        yield return null;
    }
}