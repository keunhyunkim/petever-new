using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;

// Reference : https://greenapple16.tistory.com/275
//             https://www.youtube.com/watch?v=H2TpEq0Hr2g

public class UploadPicture : MonoBehaviour
{
    public RawImage rawImage; // take image RawImage
    public Button PlusBtn;


    // Start is called before the first frame update
    void Start()
    {
        PlusBtn.onClick.AddListener(delegate { GetImage(); });
    }

    // Update is called once per frame
    void Update()
    {

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