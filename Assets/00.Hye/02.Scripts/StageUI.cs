using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StageUI : MonoBehaviour
{
    public GameObject optionUI;
    public GameObject clearUI;
    public GameObject overUI;

    private void Start()
    {
        if (optionUI) optionUI.SetActive(false);
        if (clearUI) clearUI.SetActive(false);
        if (overUI) overUI.SetActive(false);

        GameManager.Instance.OnClear.AddListener(ShowClearUI);
        GameManager.Instance.OnOver.AddListener(ShowOverUI);
    }

    public void ShowClearUI()
    {
        if (optionUI) optionUI.SetActive(false);
        if (clearUI) clearUI.SetActive(true);
        if (overUI) overUI.SetActive(false);
    }

    public void ShowOverUI()
    {
        if (optionUI) optionUI.SetActive(false);
        if (clearUI) clearUI.SetActive(false);
        if (overUI) overUI.SetActive(true);
    }

}
