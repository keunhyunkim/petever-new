using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePanelButton : MonoBehaviour
{
    private PanelManager _panelManager;

    public void Start()
    {
        _panelManager = PanelManager.Instance;
    }
    public void DoHidePanel()
    {
        _panelManager.HideLastPanel();
    }

}
