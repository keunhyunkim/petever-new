using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

// Helper methods used to set drawing settings
public class DrawingSettings : MonoBehaviour
{
    public static bool isCursorOverUI = false;
    public float Transparency = 1f;

    GameObject ManCharacter = null;
    GameObject ManHead;
    GameObject mainCanvas = null;
    GameObject mainEventSystem = null;

    void Start()
    {
        ManCharacter = GameObject.Find("Man");
        ManHead = GameObject.Find("head");
        mainCanvas = GameObject.Find("MainCanvas");
        mainEventSystem = GameObject.Find("MainEventSystem");

        ManHead.GetComponent<SkinnedMeshRenderer>().enabled = false;
        mainEventSystem.GetComponent<EventSystem>().enabled = false;
        mainCanvas.GetComponent<Canvas>().enabled = false;

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("CanvasScene"));
    }

    // Changing pen settings is easy as changing the static properties Drawable.Pen_Colour and Drawable.Pen_Width
    public void SetMarkerColour(Color new_color)
    {
        Drawable.Pen_Colour = new_color;
    }
    // new_width is radius in pixels
    public void SetMarkerWidth(int new_width)
    {
        Drawable.Pen_Width = new_width;
    }
    public void SetMarkerWidth(float new_width)
    {
        SetMarkerWidth((int)new_width);
    }

    public void SetTransparency(float amount)
    {
        Transparency = amount;
        Color c = Drawable.Pen_Colour;
        c.a = amount;
        Drawable.Pen_Colour = c;
    }


    // Call these these to change the pen settings
    public void SetMarkerRed()
    {
        Color c = Color.red;
        c.a = Transparency;
        SetMarkerColour(c);
        Drawable.drawable.SetPenBrush();
    }
    public void SetMarkerGreen()
    {
        Color c = Color.green;
        c.a = Transparency;
        SetMarkerColour(c);
        Drawable.drawable.SetPenBrush();
    }
    public void SetMarkerBlue()
    {
        Color c = Color.blue;
        c.a = Transparency;
        SetMarkerColour(c);
        Drawable.drawable.SetPenBrush();
    }
    public void SetEraser()
    {
        SetMarkerColour(new Color(255f, 255f, 255f, 255f));
    }

    public void SetEraseAll()
    {
        Drawable.drawable.ResetCanvas();
    }

    public void PartialSetEraser()
    {
        SetMarkerColour(new Color(255f, 255f, 255f, 0.5f));
    }

    IEnumerator<object> LoadBack(string scene)
    {
        Scene currentScene = SceneManager.GetActiveScene();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene));

        SceneManager.MoveGameObjectToScene(ManCharacter, SceneManager.GetSceneByName(scene));
        SceneManager.MoveGameObjectToScene(mainEventSystem, SceneManager.GetSceneByName(scene));
        SceneManager.MoveGameObjectToScene(mainCanvas, SceneManager.GetSceneByName(scene));

        SceneManager.UnloadSceneAsync(currentScene);
    }

    public void MoveBackToHome()
    {
        ManHead.GetComponent<SkinnedMeshRenderer>().enabled = true;
        mainEventSystem.GetComponent<EventSystem>().enabled = true;
        mainCanvas.GetComponent<Canvas>().enabled = true;
        
        StartCoroutine(LoadBack("newScene"));
    }

    public void CaptureScreen()
    {
        //Reference - https://eunjin3786.tistory.com/521
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        string fileName = "MYDRAW-" + timestamp + ".png";
        
        //FIX ME : Save the captured Image to PC
        CaptureScreenForPC(fileName);
    
    }

    private void CaptureScreenForPC(string fileName)
    {
        ScreenCapture.CaptureScreenshot("MyDraws/" + fileName);
    }

    private void CaptureScreenForMobile(string fileName)
    {

    }
}