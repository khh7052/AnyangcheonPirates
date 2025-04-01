using System.Collections;
using TMPro;
using UnityEngine;

public class SelectMath : MonoBehaviour
{
    public TextMeshProUGUI[] texts; // ������ �ؽ�Ʈ���� �迭�� ����
    public float blinkDuration = 1.0f; // ������ ����Ǵ� ����
    public Color blinkColor = Color.red; // ������ ���� ����

    private Coroutine[] blinkCoroutines;
    private Color[] originalColors;

    void Start()
    {
        // �ؽ�Ʈ �迭�� ������ ũ���� �ڷ�ƾ �迭 �ʱ�ȭ
        blinkCoroutines = new Coroutine[texts.Length];
        originalColors = new Color[texts.Length];

        // �� �ؽ�Ʈ�� ���� ���� ����
        for (int i = 0; i < texts.Length; i++)
        {
            originalColors[i] = texts[i].color;
        }
    }

    void Update()
    {
        // 1�� Ű �Է� ó��
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartBlinking(0);
        }

        // 2�� Ű �Է� ó��
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartBlinking(1);
        }

        // 3�� Ű �Է� ó��
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartBlinking(2);
        }

        // 4�� Ű �Է� ó��
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            StartBlinking(3);
        }
    }

    void StartBlinking(int index)
    {
        // ������ ���� ���� ��� �����̴� �ڷ�ƾ ����
        StopAllBlinkingCoroutines();

        // ���õ� �ؽ�Ʈ�� �����̴� �ڷ�ƾ ����
        if (index < texts.Length)
        {
            blinkCoroutines[index] = StartCoroutine(BlinkText(texts[index], originalColors[index]));
        }
    }

    void StopAllBlinkingCoroutines()
    {
        for (int i = 0; i < blinkCoroutines.Length; i++)
        {
            if (blinkCoroutines[i] != null)
            {
                StopCoroutine(blinkCoroutines[i]);
                texts[i].color = originalColors[i]; // �ؽ�Ʈ ������ ���� �������� ����
                blinkCoroutines[i] = null;
            }
        }
    }

    IEnumerator BlinkText(TextMeshProUGUI text, Color originalColor)
    {
        while (true)
        {
            float elapsedTime = 0f;

            // ������ ���� ���󿡼� �����̴� �������� ����
            while (elapsedTime < blinkDuration)
            {
                text.color = Color.Lerp(originalColor, blinkColor, elapsedTime / blinkDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            elapsedTime = 0f;

            // ������ �����̴� ���󿡼� ���� �������� ����
            while (elapsedTime < blinkDuration)
            {
                text.color = Color.Lerp(blinkColor, originalColor, elapsedTime / blinkDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}
