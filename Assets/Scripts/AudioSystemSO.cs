using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSystem", menuName = "Audio/Audio System")]
public class AudioSystemSO : ScriptableObject
{
    public SoundSO[] Sounds;
    private Dictionary<SoundName, SoundSO> _soundDictionary;
    float MasterVolume;

    private void OnEnable()
    {
        InitializeSoundDictionary();
    }
    private void InitializeSoundDictionary()
    {
        _soundDictionary = new Dictionary<SoundName, SoundSO>();
        foreach (var sound in Sounds)
        {
            if (!_soundDictionary.ContainsKey(sound.Name))
            {
                _soundDictionary.Add(sound.Name, sound);
            }
            else
            {
                Debug.LogWarning($"Duplicate sound name detected: {sound.Name}. Only the first one will be used.");
            }
        }
    }
    public void Play(SoundName name) 
    {
        if (_soundDictionary.TryGetValue(name, out SoundSO sound))
        {
            sound.PlaySource.Play();
        }
        else
        {
            Debug.LogWarning($"Sound {name} not found.");
        }
    }

    public void PlayUISound(SoundName name)
    {
        if (_soundDictionary.TryGetValue(name, out SoundSO sound))
        {
            sound.PlaySource.PlayOneShot(sound.AudioClip, sound.Volume);
        }
        else
        {
            Debug.LogWarning($"Sound {name} not found.");
        }
    }


    public void StopAllAudio() 
    {
        foreach (var sound in Sounds)
        {
            sound.PlaySource.Stop();
        }
    }

    public void SetVolume(SoundName name, float targetVolume)
    {
        if (_soundDictionary.TryGetValue(name, out SoundSO sound))
        {
            sound.PlaySource.volume = targetVolume;
        }
        else
        {
            Debug.LogWarning($"Sound {name} not found.");
        }
    }

    public void SetVolumeOfType(SoundType type, float targetVolume)
    {
        foreach (var sound in Sounds)
        {
            if (!(sound.Type == type)) { continue; }
            sound.PlaySource.volume = targetVolume * MasterVolume;
        }
    }

    public void SetPitch(SoundName name, float targetPitch)
    {
        if (_soundDictionary.TryGetValue(name, out SoundSO sound))
        {
            sound.PlaySource.pitch = targetPitch;
        }
        else
        {
            Debug.LogWarning($"Sound {name} not found.");
        }
    }
    public void SetPitchOfType(SoundType type, float targetPitch)
    {
        foreach (var sound in Sounds)
        {
            if (!(sound.Type == type)) { continue; }
            sound.PlaySource.volume = targetPitch;
        }
    }
    public void PlayWithPitchVariation(SoundName name, float pitchVariation = 0.1f)
    {
        if (_soundDictionary.TryGetValue(name, out SoundSO sound))
        {
            sound.PlaySource.pitch = 1f + UnityEngine.Random.Range(-pitchVariation, pitchVariation);
            sound.PlaySource.Play();
        }
        else
        {
            Debug.LogWarning($"Sound {name} not found.");
        }
    }

    public void PauseSound(SoundName name)
    {
        if (_soundDictionary.TryGetValue(name, out SoundSO sound))
        {
            sound.PlaySource.Pause();
        }
        else
        {
            Debug.LogWarning($"Sound {name} not found.");
        }
    }
    public void PauseSoundsOfType(SoundType type)
    {
        foreach (var sound in Sounds)
        {
            if (!(sound.Type == type)) { return; }
            sound.PlaySource.Pause();
        }
    }

    public void ResumeSound(SoundName name)
    {
        if (_soundDictionary.TryGetValue(name, out SoundSO sound))
        {
            sound.PlaySource.UnPause();
        }
        else
        {
            Debug.LogWarning($"Sound {name} not found.");
        }
    }
    public void ResumeSoundsOfType(SoundType type)
    {
        foreach (var sound in Sounds)
        {
            if (!(sound.Type == type)) { return; }
            sound.PlaySource.UnPause();
        }
    }

    public void SetMasterVolume(float masterVolume)
    {
        MasterVolume = masterVolume;
        foreach (var sound in Sounds)
        {
            sound.PlaySource.volume = MasterVolume * sound.Volume;
        }
    }
}
