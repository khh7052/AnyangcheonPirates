using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class StringUtility
{
    public static string[] SplitString(string input)
    {
        // ���� ǥ������ ����Ͽ� ���ڿ� ���� �������� split
        MatchCollection matches = Regex.Matches(input, @"\d+|[a-zA-Z]+");
        string[] result = new string[matches.Count];

        for (int i = 0; i < matches.Count; i++)
        {
            result[i] = matches[i].Value;
        }

        return result;
    }
}
