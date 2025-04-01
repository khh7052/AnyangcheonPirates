using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerDelegate : MonoBehaviour
{
    private SoundManager soundManager;

    private void OnEnable()
    {
        soundManager = SoundManager.Instance;

        if (soundManager == null)
            return;

        soundManager.Init();
    }


    public void PlayBGM(string soundName)
    {
        if (soundManager == null)
            return;

        soundManager.PlayBGM(soundName);
    }

    public void PlaySFX(string soundName)
    {
        if (soundManager == null)
            return;

        soundManager.PlaySFX(soundName);
    }

    public void ToggleBGM()
    {
        if (soundManager == null)
            return;

        soundManager.ToggleBGM();
    }

    public void ToggleSFX()
    {
        if (soundManager == null)
            return;

        soundManager.ToggleSFX();
    }

}
