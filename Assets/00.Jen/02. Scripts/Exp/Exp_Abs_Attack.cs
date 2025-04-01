using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Exp_Abs_Attack : MonoBehaviour
{
    public GameObject attackPoint;
    public GameObject absAttackPrefab;
    public Animator characterAnimator;  // 캐릭터 애니메이터

    public Slider amplitudeSlider;
    public TMP_Text amplitudeText;

    private float amplitude = 0.0f;  // 초기 진폭
    private float speed = 5.0f;      // 초기 속도

    public GameObject Key_Up;
    public GameObject Key_Down;

    private void OnEnable()
    {
        // 초기 슬라이더 값 설정
        InitializeSlider(amplitudeSlider, 0f, 5f, amplitude, OnAmplitudeChanged);

        // 공격 시작 코루틴
        StartCoroutine(AbsAttackRoutine());

        // 초기 텍스트 설정
        UpdateText(amplitudeText, amplitudeSlider);

        // 값 자동 증가 코루틴 시작
        StartCoroutine(AutoIncreaseValues());
    }

    void Update()
    {
        // 슬라이더 값이 변경되면 텍스트 업데이트
        UpdateText(amplitudeText, amplitudeSlider);
    }

    IEnumerator AbsAttackRoutine()
    {
        while (true)
        {
            // 애니메이션 트리거
            characterAnimator.SetTrigger("Jab");

            // Abs 공격 생성
            SpawnAttack(false); // 오른쪽 이동
            SpawnAttack(true);  // 왼쪽 이동

            // 1초마다 공격
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator AutoIncreaseValues()
    {
        while (true)
        {
            // amplitude 0에서 5로 증가
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

            // amplitude 5에서 0로 감소
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
