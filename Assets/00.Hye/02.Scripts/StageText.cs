using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class StageText : MonoBehaviour
{
    private TMP_Text text;

    void Start()
    {
        text = GetComponent<TMP_Text>();
        text.text = "";

        string stageName = SceneManager.GetActiveScene().name;
        string[] result = StringUtility.SplitString(stageName);

        foreach (string str in result)
        {
            text.text += str.ToUpper() + " ";
        }

        text.text = text.text.Trim();
    }
}
