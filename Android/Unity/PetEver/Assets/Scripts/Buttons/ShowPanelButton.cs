using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanelButton : MonoBehaviour
{

    public string PanelId;

    private PanelManager _panelManager;

    public void Start()
    {
        _panelManager = PanelManager.Instance;
    }
    public void DoShowPanel()
    {
        _panelManager.ShowPanel(PanelId);
    }

}
