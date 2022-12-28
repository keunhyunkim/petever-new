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

    IEnumerator<object> LoadBack()
    {
        // Set the current Scene to be able to unload it later
        Scene currentScene = SceneManager.GetActiveScene();

        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("newScene", LoadSceneMode.Additive);

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);
    }

    //Call this whenever you want to load the previous scene
    //It will remove the current scene from the history and then load the new last scene in the history
    //It will return false if we have not moved between scenes enough to have stored a previous scene in the history
    public bool PreviousScene()
    {
        bool returnValue = false;

        if (CallCanvas.sceneHistory.Count >= 2)  //Checking that we have actually switched scenes enough to go back to a previous scene
        {
            returnValue = true;
            CallCanvas.sceneHistory.RemoveAt(CallCanvas.sceneHistory.Count - 1);
            SceneManager.LoadScene(CallCanvas.sceneHistory[CallCanvas.sceneHistory.Count - 1]);
        }
 
        return returnValue;
    }

    public void MoveBackToHome()
    {
        // StartCoroutine(LoadBack());
        PreviousScene();
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