using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSystemSO AudioSystemSO;

    public FloatVariable MasterVolume;
    public FloatVariable SFXVolume;
    public FloatVariable BGMVolume;

    private void Start()
    {
        foreach (var sound in AudioSystemSO.Sounds)
        {
            sound.PlaySource = gameObject.AddComponent<AudioSource>();

            sound.PlaySource.clip = sound.AudioClip;
            sound.PlaySource.volume = sound.Volume;
            sound.PlaySource.pitch = sound.Pitch;
            sound.PlaySource.loop = sound.Loop;
        }
        AudioSystemSO.MasterVolume = MasterVolume.Value;
        AudioSystemSO.SFXVolume = SFXVolume.Value;
        AudioSystemSO.BGMVolume = BGMVolume.Value;
        AudioSystemSO.Play(SoundName.MainTheme);
        AudioManager.Instance.AudioSystemSO.SetMasterVolume(MasterVolume.Value);
    }

    public void FadeIn(SoundName name, float duration)
    {
        StartCoroutine(FadeCoroutine(name, 0f, 1f, duration));
    }

    public void FadeOut(SoundName name, float duration)
    {
        StartCoroutine(FadeCoroutine(name, 1f, 0f, duration));
    }

    private IEnumerator FadeCoroutine(SoundName name, float startVolume, float endVolume, float duration)
    {
        SoundSO sound = Array.Find(AudioSystemSO.Sounds, s => s.Name == name);
        if (sound == null) yield break;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            sound.PlaySource.volume = Mathf.Lerp(startVolume, endVolume, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        sound.PlaySource.volume = endVolume;
    }
}
