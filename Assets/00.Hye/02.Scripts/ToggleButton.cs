using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum ToggleType
{
    BGM,
    SFX
}
public class ToggleButton : MonoBehaviour
{
    public UnityEvent OnToggle = new();

    public ToggleType type;
    public Image buttonImage;
    public Sprite onSprite;
    public Sprite offSprite;
    public bool isOn = false;


    void Start()
    {
        Init();
    }

    void Init()
    {
        if(type == ToggleType.BGM)
        {
            isOn = SoundManager.Instance.BGM_Mute;
        }
        else if (type == ToggleType.SFX)
        {
            isOn = SoundManager.Instance.SFX_Mute;
        }

        ImageUpdate();
    }

    public void Toggle()
    {
        isOn = !isOn;
        ImageUpdate();

        OnToggle.Invoke();
    }

    void ImageUpdate()
    {
        buttonImage.sprite = isOn ? onSprite : offSprite;
    }
}
