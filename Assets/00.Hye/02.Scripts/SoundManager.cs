using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Sound
{
    public AudioClip[] clips;
    public string name;
    public float volume = 1;
}

public class SoundManager : Singleton<SoundManager>
{
    public AudioMixer audioMixer;
    public Sound[] sounds;
    private Dictionary<string, Sound> allSound = new();
    public AudioSource bgmSource;
    public AudioSource sfxSource;


    public bool BGM_Mute
    {
        get
        {
            return bgmMute;
        }
        set
        {
            bgmMute = value;
            float volume = value ? -80 : 0;
            SetBGMVolume(volume);
        }
    }

    public bool SFX_Mute
    {
        get
        {
            return sfxMute;
        }
        set
        {
            sfxMute = value;
            float volume = value ? -80 : 0;
            SetSFXVolume(volume);
        }
    }

    private bool bgmMute = false;
    private bool sfxMute = false;

    private bool isInit = false;

    private void Awake()
    {
        Init();
    }

    private void OnLevelWasLoaded(int level)
    {
        BGMUpdate();
    }

    public void Init()
    {
        if (isInit) return;
        isInit = true;

        foreach (var sound in sounds)
	{
            allSound.Add(sound.name, sound);
        }

        BGMUpdate();
    }

    void BGMUpdate()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        string[] ss = StringUtility.SplitString(sceneName);

        if (ss[0] == "Stage")
        {
            PlayBGM("Stage");
        }
        else
        {
            PlayBGM(sceneName);
        }
    }

    public void ToggleBGM()
    {
        BGM_Mute = !BGM_Mute;
    }

    public void ToggleSFX()
    {
        print(SFX_Mute);
        SFX_Mute = !SFX_Mute;
    }

    public void PlayBGM(string soundName)
    {
        if (BGM_Mute) return;
        if (isInit == false)
            Init();

        if (!allSound.ContainsKey(soundName)) return;

        AudioClip clip = null;

        if (allSound[soundName].clips.Length == 1)
        {
            clip = allSound[soundName].clips[0];
        }
        else
        {
            clip = allSound[soundName].clips[Random.Range(0, allSound[soundName].clips.Length)];
        }

        bgmSource.clip = clip;
        SetBGMVolume(allSound[soundName].volume);
        bgmSource.Play();
    }

    public void PlaySFX(string soundName)
    {
        if (SFX_Mute) return;
        if (isInit == false)
            Init();

        if (!allSound.ContainsKey(soundName)) return;

        SetSFXVolume(allSound[soundName].volume);

        AudioClip clip = null;

        if (allSound[soundName].clips.Length == 1)
        {
            clip = allSound[soundName].clips[0];
        }
        else
        {
            clip = allSound[soundName].clips[Random.Range(0, allSound[soundName].clips.Length)];
        }

        sfxSource.PlayOneShot(clip);
    }

    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGM", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
    }


}
