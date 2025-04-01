using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplainManager : MonoBehaviour
{
    public ExplainPanel[] panels;

    private void OnEnable()
    {
        PanelUpdate(0);
    }

    public void PanelUpdate(int index)
    {
	foreach (var panel in panels)
        {
            panel.explainObject.SetActive(false);
            panel.panelButton.interactable = true;
        }

        panels[index].explainObject.SetActive(true);
        panels[index].panelButton.interactable = false;
    }

    public void PanelUpdate(GameObject panel)
    {
        foreach (var p in panels)
        {
            p.explainObject.SetActive(p == panel);
            p.panelButton.interactable = p != panel;
        }

    }

}
