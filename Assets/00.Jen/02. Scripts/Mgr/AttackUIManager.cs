using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class AttackUIManager : MonoBehaviour
{
    public Slider sinAmplitudeSlider;
    public TextMeshProUGUI sinAmplitudeText;
    public Slider sinFrequencySlider;
    public TextMeshProUGUI sinFrequencyText;

    public Slider cosAmplitudeSlider;
    public TextMeshProUGUI cosAmplitudeText;
    public Slider cosFrequencySlider;
    public TextMeshProUGUI cosFrequencyText;

    public Slider tanAmplitudeSlider;
    public TextMeshProUGUI tanAmplitudeText;
    public Slider tanFrequencySlider;
    public TextMeshProUGUI tanFrequencyText;

    public Slider absAmplitudeSlider;
    public TextMeshProUGUI absAmplitudeText;

    public AttackManager attackManager;
    public float sliderStep = 0.1f;

    public GameObject start_Star_UI;

    private Coroutine adjustCoroutine;

    void Start()
    {
        InitializeSlider(sinAmplitudeSlider, 0f, 5f, attackManager.sinAttackAmplitude, OnSinAmplitudeChanged);
        InitializeSlider(sinFrequencySlider, 0f, 3f, attackManager.sinAttackFrequency, OnSinFrequencyChanged);
        InitializeSlider(cosAmplitudeSlider, 0f, 5f, attackManager.cosAttackAmplitude, OnCosAmplitudeChanged);
        InitializeSlider(cosFrequencySlider, 0f, 3f, attackManager.cosAttackFrequency, OnCosFrequencyChanged);
        InitializeSlider(tanAmplitudeSlider, 1f, 3f, attackManager.tanAttackAmplitude, OnTanAmplitudeChanged);
        InitializeSlider(tanFrequencySlider, 1f, 3f, attackManager.tanAttackFrequency, OnTanFrequencyChanged);
        InitializeSlider(absAmplitudeSlider, 0f, 5f, attackManager.absAttackAmplitude, OnAbsAmplitudeChanged);

        UpdateAllTexts();

        Destroy(start_Star_UI, 5f);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (adjustCoroutine == null)
                adjustCoroutine = StartCoroutine(AdjustSliderValueOverTime(-sliderStep, sinAmplitudeSlider, cosAmplitudeSlider, tanAmplitudeSlider, absAmplitudeSlider));
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            if (adjustCoroutine == null)
                adjustCoroutine = StartCoroutine(AdjustSliderValueOverTime(sliderStep, sinAmplitudeSlider, cosAmplitudeSlider, tanAmplitudeSlider, absAmplitudeSlider));
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (adjustCoroutine == null)
                adjustCoroutine = StartCoroutine(AdjustSliderValueOverTime(sliderStep, sinFrequencySlider, cosFrequencySlider, tanFrequencySlider));
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (adjustCoroutine == null)
                adjustCoroutine = StartCoroutine(AdjustSliderValueOverTime(-sliderStep, sinFrequencySlider, cosFrequencySlider, tanFrequencySlider));
        }
        else
        {
            if (adjustCoroutine != null)
            {
                StopCoroutine(adjustCoroutine);
                adjustCoroutine = null;
            }
        }
    }

    IEnumerator AdjustSliderValueOverTime(float step, params Slider[] sliders)
    {
        while (true)
        {
            AdjustSliderValue(step, sliders);
            yield return new WaitForSeconds(0.1f);
        }
    }

    void InitializeSlider(Slider slider, float minValue, float maxValue, float initialValue, UnityEngine.Events.UnityAction<float> callback)
    {
        slider.minValue = minValue;
        slider.maxValue = maxValue;
        slider.value = initialValue;
        slider.onValueChanged.AddListener(callback);
    }

    void AdjustSliderValue(float step, params Slider[] sliders)
    {
        foreach (var slider in sliders)
        {
            slider.value = Mathf.Clamp(slider.value + step, slider.minValue, slider.maxValue);
        }
    }

    void UpdateText(TextMeshProUGUI text, Slider slider)
    {
        text.text = $"{slider.value:F1} / {slider.maxValue:F1}";
    }

    void UpdateAllTexts()
    {
        UpdateText(sinAmplitudeText, sinAmplitudeSlider);
        UpdateText(sinFrequencyText, sinFrequencySlider);
        UpdateText(cosAmplitudeText, cosAmplitudeSlider);
        UpdateText(cosFrequencyText, cosFrequencySlider);
        UpdateText(tanAmplitudeText, tanAmplitudeSlider);
        UpdateText(tanFrequencyText, tanFrequencySlider);
        UpdateText(absAmplitudeText, absAmplitudeSlider);
    }

    void OnSinAmplitudeChanged(float value)
    {
        attackManager.sinAttackAmplitude = value;
        UpdateText(sinAmplitudeText, sinAmplitudeSlider);
    }

    void OnSinFrequencyChanged(float value)
    {
        attackManager.sinAttackFrequency = value;
        UpdateText(sinFrequencyText, sinFrequencySlider);
    }

    void OnCosAmplitudeChanged(float value)
    {
        attackManager.cosAttackAmplitude = value;
        UpdateText(cosAmplitudeText, cosAmplitudeSlider);
    }

    void OnCosFrequencyChanged(float value)
    {
        attackManager.cosAttackFrequency = value;
        UpdateText(cosFrequencyText, cosFrequencySlider);
    }

    void OnTanAmplitudeChanged(float value)
    {
        attackManager.tanAttackAmplitude = value;
        UpdateText(tanAmplitudeText, tanAmplitudeSlider);
    }

    void OnTanFrequencyChanged(float value)
    {
        attackManager.tanAttackFrequency = value;
        UpdateText(tanFrequencyText, tanFrequencySlider);
    }

    void OnAbsAmplitudeChanged(float value)
    {
        attackManager.absAttackAmplitude = value;
        UpdateText(absAmplitudeText, absAmplitudeSlider);
    }
}
