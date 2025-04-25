using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSystemSO AudioSystemSO;

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

        FadeCoroutine(SoundName.MainTheme, 0, 1, 5);
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
