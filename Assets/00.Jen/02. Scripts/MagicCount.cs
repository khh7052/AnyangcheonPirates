using UnityEngine;
using TMPro;
using System.Collections;

public class MagicCount : MonoBehaviour
{
    public int initialMagicCount = 5; // �ʱ� ���� ī��Ʈ
    public static bool magicCountBool = true; // ���� ��� ���� ����
    public TextMeshProUGUI magicCountText; // ���� ī��Ʈ�� ǥ���� TextMeshProUGUI

    public static int currentMagicCount;

    private Color originalColor;
    public Color warningColor;

    void Start()
    {
        currentMagicCount = 0;
        originalColor = magicCountText.color;
        magicCountBool = false;
        UpdateMagicCountText();
        StartCoroutine(IncreaseMagicCountOverTime(initialMagicCount, 2f));
    }

    private void Update()
    {
        if (currentMagicCount == 0)
        {
            magicCountBool = false;
        }
        UpdateMagicCountText();
    }

    // ���� ī��Ʈ�� �ؽ�Ʈ�� ������Ʈ�ϴ� �޼���
    void UpdateMagicCountText()
    {
        magicCountText.color = currentMagicCount <= 1 ? warningColor : originalColor;
        magicCountText.text = currentMagicCount.ToString();
    }

    // ���� ī��Ʈ�� �����ϴ� �޼��� (���ϴ� ���)
    public void ResetMagicCount(int newMagicCount)
    {
        StopAllCoroutines();
        initialMagicCount = newMagicCount;
        currentMagicCount = 0;
        magicCountBool = false;
        UpdateMagicCountText();
        StartCoroutine(IncreaseMagicCountOverTime(newMagicCount, 2f));
    }

    // ���� ī��Ʈ�� 4�� ���� ������ ������Ű�� �ڷ�ƾ
    IEnumerator IncreaseMagicCountOverTime(int targetCount, float duration)
    {
        float elapsedTime = 0f;
        int startCount = currentMagicCount;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            currentMagicCount = (int)Mathf.Lerp(startCount, targetCount, elapsedTime / duration);
            UpdateMagicCountText();
            yield return null;
        }

        currentMagicCount = targetCount;
        magicCountBool = true;
        UpdateMagicCountText();
    }
}
