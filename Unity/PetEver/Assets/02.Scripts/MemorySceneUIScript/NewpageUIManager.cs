using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


// Reference : https://greenapple16.tistory.com/275
//             https://www.youtube.com/watch?v=H2TpEq0Hr2g

public class NewpageUIManager : MonoBehaviour
{
    public RawImage rawImage; // take image RawImage
    public Button ImageBtn, StickerBtn, TextBtn, XBtn;
    public static GameObject UserInputTextBundle, StickerInventory, rawImageText;
    private static CanvasGroup newPageUI;
    public static Transform[] trashCan;
    // Start is called before the first frame update
    void Start()
    {
        if (rawImage != null)
        {
            rawImageText = rawImage.transform.GetChild(0).gameObject;
        }

        newPageUI = gameObject.GetComponent<CanvasGroup>();

        ImageBtn.onClick.AddListener(delegate { GetImage(); });
        TextBtn.onClick.AddListener(delegate { GetText(); });
        StickerBtn.onClick.AddListener(delegate { GetSticker(); });
        XBtn.onClick.AddListener(delegate { CloseUI(); });

    }


    private static void HideCanvasGroup(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
    public static void CloseUI()
    {
        trashCan = GameObject.Find("TextAndSticker").GetComponentsInChildren<Transform>();    

        if(trashCan != null)
        {
            for(int i = 1; i < trashCan.Length; i++)
            {
                Destroy(trashCan[i].gameObject);
            }
        }
        HideCanvasGroup(newPageUI);
    }

    public void GetText()
    {
        UserInputTextBundle = Resources.Load<GameObject>("Prefabs/UserInputTextBundle");
        UserInputTextBundle = Instantiate(UserInputTextBundle, GameObject.Find("TextAndSticker").GetComponent<RectTransform>());
    }

    public void GetSticker()
    {
        StickerInventory = Resources.Load<GameObject>("Prefabs/StickerInventory");
        StickerInventory = Instantiate(StickerInventory, GameObject.Find("TextAndSticker").GetComponent<RectTransform>());
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

        Texture2D texture = new Texture2D(846, 1003);
        texture.LoadImage(tempImage); // transfer byte array to texture 2D

        rawImage.texture = texture;
        rawImage.SetNativeSize();
        ImageSizeSetting(rawImage, 846, 1003);
        Destroy(rawImageText);

        yield return null;
    }

    void ImageSizeSetting(RawImage img, float x, float y)
    {
        var imgX = img.rectTransform.sizeDelta.x;
        var imgY = img.rectTransform.sizeDelta.y;

        if (x / y > imgX / imgY) // if image height is longer than width 
        {
            img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, y);
            img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, imgX * (y / imgY));
        }
        else // if image width is longer than height 
        {
            img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x);
            img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, imgY * (x / imgX));
        }
    }
}