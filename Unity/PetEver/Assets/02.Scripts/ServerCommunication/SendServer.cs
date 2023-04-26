using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using Defective.JSON;

using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class SendServer : MonoBehaviour
{
    private string uploadUrl = "http://3.35.242.182:3200/nukki";
    RawImage nkImg;
    private float clickTime;
    private float minClickTime = 1;
    private bool isClick;

    public void LongClickStart()
    {
        isClick = true;
    }

    public void LongClickEnd()
    {
        isClick = false;
        if (clickTime >= minClickTime) {
            sendImgToServer();
        }
    }

    private void Start()
    {
        nkImg = GameObject.Find("AddpicOutline").GetComponent<RawImage>();
    }

    private void Update()
    {
        if (isClick) {
            clickTime += Time.deltaTime;
        } else {
            clickTime = 0;
        }
    }

    public void sendImgToServer()
    {
        StartCoroutine(UploadImage());
        // NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        // {
        // }, "Select an image", "image/*");
    }

    IEnumerator UploadImage()
    {
        // Load the selected image into a texture
        Texture2D texture = (Texture2D)nkImg.texture;//NativeGallery.LoadImageAtPath(path, 1024, false);

        // Convert the texture into a byte array
        byte[] bytes = texture.EncodeToJPG();

        // Create a JSON object with the image data
        JSONObject json = new JSONObject();
        json.AddField("name", "rembg.jpg");//Path.GetFileName(path));
        json.AddField("target_img", System.Convert.ToBase64String(bytes));

        // Create a UnityWebRequest object with the JSON data
        UnityWebRequest request = new UnityWebRequest(uploadUrl, "POST");
        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(json.ToString());
        UploadHandlerRaw uploadHandler = new UploadHandlerRaw(jsonBytes);
        uploadHandler.contentType = "application/json";
        request.uploadHandler = uploadHandler;
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Accept", "application/json");

        // Send the request and wait for a response
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Image upload failed: " + request.error);
        }
        else
        {
            Debug.Log("Image uploaded successfully!");

            // Extract the image data from the response
            JSONObject responseJson = new JSONObject(request.downloadHandler.text);
            string imageData = responseJson.GetField("nukki_img").stringValue;

            // Convert the image data from Base64 to a byte array
            byte[] imageBytes = System.Convert.FromBase64String(imageData);

            // Create a new texture from the image data
            Texture2D downloadedTexture = new Texture2D(2, 2);
            downloadedTexture.LoadImage(imageBytes);

            string saveImagePath = Application.persistentDataPath + "/Image";
            if (Directory.Exists(saveImagePath)) // if file to save image is not exist, make path first
            {
                Directory.CreateDirectory(saveImagePath);
            }
            string imageName = "nukki";
            File.WriteAllBytes(saveImagePath + imageName + ".jpg", imageBytes); // set path and file name to save image

            Texture2D texture2 = new Texture2D(846, 1003);
            texture2.LoadImage(imageBytes); // transfer byte array to texture 2D

            nkImg.texture = texture2;
            nkImg.SetNativeSize();

            // Assign the texture to a material
            GetComponent<Renderer>().material.mainTexture = downloadedTexture;

            Debug.Log("Image downloaded successfully!");
        }
    }
}
