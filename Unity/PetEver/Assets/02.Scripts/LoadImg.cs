using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System.IO;


public class LoadImg : MonoBehaviour
{
    // void UpdateImage(string path)
    // {

    //     Texture2D tex = loadImage(image.rectTransform.sizeDelta, path);

    //     // texture2d로 sprite 생성
    //     image.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
    // }

    // // 이미지 파일을 texture2d로 변환
    // private static Texture2D loadImage(Vector2 size, string filePath)
    // {
       
    //     byte[] bytes = File.ReadAllBytes(filePath);
    //     Texture2D texture = new Texture2D((int)size.x, (int)size.y, TextureFormat.RGB24, false);
    //     texture.filterMode = FilterMode.Trilinear;
    //     texture.LoadImage(bytes);

    //     return texture;
    // }

    // private void callbackForGallery(string path)
    // {
    //       UpdateImage(path);
    // }

    // void ONCLICK()
    // {
    //     NativeGallery.GetImageFromGallery(callbackForGallery);
    // }
    GameObject myImage;
    public RawImage rawImage;

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

    public void showMyImage()
    {
        GetImage();
        myImage.GetComponent<Image>().sprite = Sprite.Create(rawImage.texture as Texture2D, new Rect(0, 0, rawImage.texture.width, rawImage.texture.height), new Vector2(0.5f, 0.5f));
    } 

    void Start()
    {
        myImage = GameObject.Find("MyImage1");
    }

}
