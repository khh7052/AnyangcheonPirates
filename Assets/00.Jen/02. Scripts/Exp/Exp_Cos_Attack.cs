using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Exp_Cos_Attack : MonoBehaviour
{
    public GameObject attackPoint;
    public GameObject cosAttackPrefab;
    public Animator characterAnimator;  // 캐릭터 애니메이터

    public Slider amplitudeSlider;
    public TMP_Text amplitudeText;
    public Slider frequencySlider;
    public TMP_Text frequencyText;

    private float amplitude = 1.0f;  // 초기 진폭
    private float frequency = 1.0f;  // 초기 빈도
    private float speed = 5.0f;      // 공격 이동 속도
    private Vector3 direction = Vector3.right;  // 공격 방향 벡터 (캐릭터의 오른쪽)

    public GameObject Key_Up;
    public GameObject Key_Down;
    public GameObject Key_Right;
    public GameObject Key_Left;

    private void OnEnable()
    {
        // 초기 슬라이더 값 설정
        InitializeSlider(amplitudeSlider, 0f, 5f, amplitude, OnAmplitudeChanged);
        InitializeSlider(frequencySlider, 0f, 3f, frequency, OnFrequencyChanged);

        // 공격 시작 코루틴
        StartCoroutine(CosAttackRoutine());

        // 초기 텍스트 설정
        UpdateText(amplitudeText, amplitudeSlider);
        UpdateText(frequencyText, frequencySlider);

        // 값 자동 증가 코루틴 시작
        StartCoroutine(AutoIncreaseValues());
    }

    void Update()
    {
        // 슬라이더 값이 변경되면 텍스트 업데이트
        UpdateText(amplitudeText, amplitudeSlider);
        UpdateText(frequencyText, frequencySlider);
    }

    IEnumerator CosAttackRoutine()
    {
        while (true)
        {
            // 애니메이션 트리거
            characterAnimator.SetTrigger("Jab");

            // Cos 공격 생성
            SpawnAttack();

            // 1초마다 공격
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator AutoIncreaseValues()
    {
        while (true)
        {
            // amplitude 1에서 5로 증가
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

            // frequency 1에서 3로 증가
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

            // amplitude 5에서 1로 감소
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

            // frequency 3에서 1로 감소
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