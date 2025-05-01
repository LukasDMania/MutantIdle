using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PauseMenuManager : Singleton<PauseMenuManager>
{
    public GameObject PauseMenuContainer;
    public GameObject Mask;

    [HideInInspector]
    public bool PauseMenuIsOpen;

    public Slider MasterV;
    public Slider SFXV;
    public Slider BGMV;

    public FloatVariable MasterVolume;
    public FloatVariable SFXVolume;
    public FloatVariable BGMVolume;

    private void Start()
    {
        CloseSettingsUI();
        Mask.SetActive(false);
    }
    public void OpenSettingsUI()
    {
        MasterV.value = MasterVolume.Value;
        SFXV.value = SFXVolume.Value;
        BGMV.value = BGMVolume.Value;
        PauseMenuContainer.SetActive(true);
        PauseMenuIsOpen = true;
        Mask.SetActive(true);
    }
    public void CloseSettingsUI()
    {
        PauseMenuContainer.SetActive(false);
        PauseMenuIsOpen = false;
        Mask.SetActive(false);
    }
    public void PlayButtonUISound() 
    {
        AudioManager.Instance.AudioSystemSO.PlayUISound(SoundName.ButtonClick_1);
    }

    public void ToggleSettingsUI()
    {
        if (PauseMenuIsOpen)
            CloseSettingsUI();
        else
            OpenSettingsUI();
    }

    public void MasterVolumeSliderChanged()
    {
        float volume = MasterV.value;
        MasterVolume.SetValue(volume);
        Debug.Log("CHANGING TO VOLUME : " + volume);
        AudioManager.Instance.AudioSystemSO.SetMasterVolume(volume);
    }
    public void SFXVolumeSliderChanged()
    {
        float volume = SFXV.value;
        SFXVolume.SetValue(volume);
        AudioManager.Instance.AudioSystemSO.SetVolumeOfType(SoundType.SFX, volume);
    }
    public void BGMVolumeSliderChanged()
    {
        float volume = BGMV.value;
        BGMVolume.SetValue(volume);
        AudioManager.Instance.AudioSystemSO.SetVolumeOfType(SoundType.Music, volume);
    }

    public void OnQuitButtonPressed() 
    {
        SaveSystemManager.Instance.QuitGame();
    }

    public AudioVolumeSaveData Save() 
    {
        return new AudioVolumeSaveData()
        {
            MasterVolume = MasterVolume.Value,
            SFXVolume = SFXVolume.Value,
            BGMVolume = BGMVolume.Value,
        };
    }

    public void Load(AudioVolumeSaveData data) 
    {
        MasterVolume.SetValue(data.MasterVolume);
        SFXVolume.SetValue(data.SFXVolume);
        BGMVolume.SetValue(data.BGMVolume);
    }
}
[Serializable]
public class AudioVolumeSaveData
{
    public float MasterVolume;
    public float SFXVolume;
    public float BGMVolume;
}
