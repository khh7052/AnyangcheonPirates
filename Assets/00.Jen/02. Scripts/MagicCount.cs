using UnityEngine;
using TMPro;
using System.Collections;

public class MagicCount : MonoBehaviour
{
    public int initialMagicCount = 5; // 초기 매직 카운트
    public static bool magicCountBool = true; // 매직 사용 가능 여부
    public TextMeshProUGUI magicCountText; // 매직 카운트를 표시할 TextMeshProUGUI

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

    // 매직 카운트를 텍스트로 업데이트하는 메서드
    void UpdateMagicCountText()
    {
        magicCountText.color = currentMagicCount <= 1 ? warningColor : originalColor;
        magicCountText.text = currentMagicCount.ToString();
    }

    // 매직 카운트를 리셋하는 메서드 (원하는 경우)
    public void ResetMagicCount(int newMagicCount)
    {
        StopAllCoroutines();
        initialMagicCount = newMagicCount;
        currentMagicCount = 0;
        magicCountBool = false;
        UpdateMagicCountText();
        StartCoroutine(IncreaseMagicCountOverTime(newMagicCount, 2f));
    }

    // 매직 카운트를 4초 동안 서서히 증가시키는 코루틴
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
