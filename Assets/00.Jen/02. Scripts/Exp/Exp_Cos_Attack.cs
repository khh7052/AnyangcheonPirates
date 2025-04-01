using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Exp_Cos_Attack : MonoBehaviour
{
    public GameObject attackPoint;
    public GameObject cosAttackPrefab;
    public Animator characterAnimator;  // ĳ���� �ִϸ�����

    public Slider amplitudeSlider;
    public TMP_Text amplitudeText;
    public Slider frequencySlider;
    public TMP_Text frequencyText;

    private float amplitude = 1.0f;  // �ʱ� ����
    private float frequency = 1.0f;  // �ʱ� ��
    private float speed = 5.0f;      // ���� �̵� �ӵ�
    private Vector3 direction = Vector3.right;  // ���� ���� ���� (ĳ������ ������)

    public GameObject Key_Up;
    public GameObject Key_Down;
    public GameObject Key_Right;
    public GameObject Key_Left;

    private void OnEnable()
    {
        // �ʱ� �����̴� �� ����
        InitializeSlider(amplitudeSlider, 0f, 5f, amplitude, OnAmplitudeChanged);
        InitializeSlider(frequencySlider, 0f, 3f, frequency, OnFrequencyChanged);

        // ���� ���� �ڷ�ƾ
        StartCoroutine(CosAttackRoutine());

        // �ʱ� �ؽ�Ʈ ����
        UpdateText(amplitudeText, amplitudeSlider);
        UpdateText(frequencyText, frequencySlider);

        // �� �ڵ� ���� �ڷ�ƾ ����
        StartCoroutine(AutoIncreaseValues());
    }

    void Update()
    {
        // �����̴� ���� ����Ǹ� �ؽ�Ʈ ������Ʈ
        UpdateText(amplitudeText, amplitudeSlider);
        UpdateText(frequencyText, frequencySlider);
    }

    IEnumerator CosAttackRoutine()
    {
        while (true)
        {
            // �ִϸ��̼� Ʈ����
            characterAnimator.SetTrigger("Jab");

            // Cos ���� ����
            SpawnAttack();

            // 1�ʸ��� ����
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator AutoIncreaseValues()
    {
        while (true)
        {
            // amplitude 1���� 5�� ����
            float elapsedTime = 0f;
            while (elapsedTime < 3f)
            {
                amplitude = Mathf.Lerp(1f, 5f, elapsedTime / 3f);
                UpdateSliderAndText(amplitudeSlider, amplitudeText, amplitude);
                ToggleKeys(Key_Right, false);
                ToggleKeys(Key_Left, false);
                ToggleKeys(Key_Up, true);
                ToggleKeys(Key_Down, false);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // frequency 1���� 3�� ����
            elapsedTime = 0f;
            while (elapsedTime < 3f)
            {
                frequency = Mathf.Lerp(1f, 3f, elapsedTime / 3f);
                UpdateSliderAndText(frequencySlider, frequencyText, frequency);
                ToggleKeys(Key_Up, false);
                ToggleKeys(Key_Down, false);
                ToggleKeys(Key_Right, false);
                ToggleKeys(Key_Left, true);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // amplitude 5���� 1�� ����
            elapsedTime = 0f;
            while (elapsedTime < 3f)
            {
                amplitude = Mathf.Lerp(5f, 1f, elapsedTime / 3f);
                UpdateSliderAndText(amplitudeSlider, amplitudeText, amplitude);
                ToggleKeys(Key_Right, false);
                ToggleKeys(Key_Left, false);
                ToggleKeys(Key_Up, false);
                ToggleKeys(Key_Down, true);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // frequency 3���� 1�� ����
            elapsedTime = 0f;
            while (elapsedTime < 3f)
            {
                frequency = Mathf.Lerp(3f, 1f, elapsedTime / 3f);
                UpdateSliderAndText(frequencySlider, frequencyText, frequency);
                ToggleKeys(Key_Up, false);
                ToggleKeys(Key_Down, false);
                ToggleKeys(Key_Right, true);
                ToggleKeys(Key_Left, false);
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

    void OnFrequencyChanged(float value)
    {
        frequency = value;
        UpdateText(frequencyText, frequencySlider);
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

    void SpawnAttack()
    {
        if (cosAttackPrefab != null && attackPoint != null)
        {
            GameObject attackInstance = Instantiate(cosAttackPrefab, attackPoint.transform.position, Quaternion.identity);
            CosWaveAttack cosWaveAttack = attackInstance.GetComponent<CosWaveAttack>();

            if (cosWaveAttack != null)
            {
                cosWaveAttack.amplitude = amplitude;
                cosWaveAttack.frequency = frequency;
                cosWaveAttack.speed = speed;
                cosWaveAttack.direction = direction;
            }
        }
    }
}