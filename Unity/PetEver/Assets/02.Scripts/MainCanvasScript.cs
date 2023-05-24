using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvasScript : MonoBehaviour
{
  
      public void VoiceButton()
    {
        DogAI.voiceButton_bool = true;
    }

    public void EscortButton()
    {
        DogEscort.welcomeEscort = true;
    }

}