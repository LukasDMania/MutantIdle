using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSystem", menuName = "Audio/Audio System")]
public class AudioSystemSO : ScriptableObject
{
    public SoundSO[] Sounds;

    public void Play(SoundName name) 
    {
        SoundSO sound = Array.Find(Sounds, sound => sound.Name == name);

        sound.PlaySource.Play();
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
        SoundSO sound = Array.Find(Sounds, sound => sound.Name == name);

        if (sound != null) { sound.PlaySource.volume = targetVolume; }
    }
    public void SetVolumeOfType(SoundType type, float targetVolume)
    {
        foreach (var sound in Sounds)
        {
            if (!(sound.Type == type)) { continue; }
            sound.PlaySource.volume = targetVolume;
        }
    }

    public void SetPitch(SoundName name, float targetPitch) 
    {
        SoundSO sound = Array.Find(Sounds, sound => sound.Name == name);

        sound.PlaySource.pitch = targetPitch;
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
        SoundSO sound = Array.Find(Sounds, s => s.Name == name);
        if (sound != null)
        {
            sound.PlaySource.pitch = 1f + UnityEngine.Random.Range(-pitchVariation, pitchVariation);
            sound.PlaySource.Play();
        }
    }

    public void PauseSound(SoundName name)
    {
        SoundSO sound = Array.Find(Sounds, s => s.Name == name);

        if (sound != null) { sound.PlaySource.Pause(); }
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
        SoundSO sound = Array.Find(Sounds, s => s.Name == name);

        if (sound != null) { sound.PlaySource.UnPause(); }
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
        foreach (var sound in Sounds)
        {
            sound.PlaySource.volume = masterVolume * sound.Volume;
        }
    }
}
