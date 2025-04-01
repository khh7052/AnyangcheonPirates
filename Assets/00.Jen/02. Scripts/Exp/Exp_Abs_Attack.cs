using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Exp_Abs_Attack : MonoBehaviour
{
    public GameObject attackPoint;
    public GameObject absAttackPrefab;
    public Animator characterAnimator;  // ĳ���� �ִϸ�����

    public Slider amplitudeSlider;
    public TMP_Text amplitudeText;

    private float amplitude = 0.0f;  // �ʱ� ����
    private float speed = 5.0f;      // �ʱ� �ӵ�

    public GameObject Key_Up;
    public GameObject Key_Down;

    private void OnEnable()
    {
        // �ʱ� �����̴� �� ����
        InitializeSlider(amplitudeSlider, 0f, 5f, amplitude, OnAmplitudeChanged);

        // ���� ���� �ڷ�ƾ
        StartCoroutine(AbsAttackRoutine());

        // �ʱ� �ؽ�Ʈ ����
        UpdateText(amplitudeText, amplitudeSlider);

        // �� �ڵ� ���� �ڷ�ƾ ����
        StartCoroutine(AutoIncreaseValues());
    }

    void Update()
    {
        // �����̴� ���� ����Ǹ� �ؽ�Ʈ ������Ʈ
        UpdateText(amplitudeText, amplitudeSlider);
    }

    IEnumerator AbsAttackRoutine()
    {
        while (true)
        {
            // �ִϸ��̼� Ʈ����
            characterAnimator.SetTrigger("Jab");

            // Abs ���� ����
            SpawnAttack(false); // ������ �̵�
            SpawnAttack(true);  // ���� �̵�

            // 1�ʸ��� ����
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator AutoIncreaseValues()
    {
        while (true)
        {
            // amplitude 0���� 5�� ����
            float elapsedTime = 0f;
            while (elapsedTime < 5f)
            {
                amplitude = Mathf.Lerp(0f, 5f, elapsedTime / 5f);
                UpdateSliderAndText(amplitudeSlider, amplitudeText, amplitude);
                ToggleKeys(Key_Up, true);
                ToggleKeys(Key_Down, false);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // amplitude 5���� 0�� ����
            elapsedTime = 0f;
            while (elapsedTime < 5f)
            {
                amplitude = Mathf.Lerp(5f, 0f, elapsedTime / 5f);
                UpdateSliderAndText(amplitudeSlider, amplitudeText, amplitude);
                ToggleKeys(Key_Up, false);
                ToggleKeys(Key_Down, true);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }

    void ToggleKeys(GameObject keyObject, bool isActive)
    {
        keyObject.SetActive(isActive);
    }

    void OnAmplitudeChanged(float value)
    {
        amplitude = value;
        UpdateText(amplitudeText, amplitudeSlider);
    }

    void InitializeSlider(Slider slider, float minValue, float maxValue, float initialValue, UnityEngine.Events.UnityAction<float> callback)
    {
        slider.minValue = minValue;
        slider.maxValue = maxValue;
        slider.value = initialValue;
        slider.onValueChanged.AddListener(callback);
    }

    void UpdateText(TMP_Text text, Slider slider)
    {
        text.text = $"{slider.value:F2} / {slider.maxValue:F2}";
    }

    void UpdateSliderAndText(Slider slider, TMP_Text text, float value)
    {
        slider.value = value;
        text.text = $"{value:F2} / {slider.maxValue:F2}";
    }

    void SpawnAttack(bool moveLeft)
    {
        if (absAttackPrefab != null && attackPoint != null)
        {
            GameObject attackInstance = Instantiate(absAttackPrefab, attackPoint.transform.position, Quaternion.identity);
            AbsWaveAttack absWaveAttack = attackInstance.GetComponent<AbsWaveAttack>();

            if (absWaveAttack != null)
            {
                absWaveAttack.amplitude = amplitude;
                absWaveAttack.speed = speed;
                absWaveAttack.moveLeft = moveLeft;
            }
        }
    }
}
