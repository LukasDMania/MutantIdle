using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound", menuName = "Audio/Sound")]
public class SoundSO : ScriptableObject
{
    public SoundName Name;
    public SoundType Type;

    public AudioClip AudioClip;

    [Range(0f, 1f)]
    public float Volume = 1;
    [Range(.1f, 3f)]
    public float Pitch = 1;

    public bool Loop;

    [HideInInspector]
    public AudioSource PlaySource;
}

public enum SoundName
{
    MainTheme,
    ButtonClick_1,
    ButtonClick_2,
    ButtonClick_3,
    LightFlicker1,
    LightFlicker2,
    LightFlicker3,
    AchievementUnlocked,
    PowerUpGlow,
    PowerUpOnClick,
    PrestigeSound,
    BodyPartAttached,
}

public enum SoundType
{
    SFX,
    Music,
    UI,
    Ambient,
}
