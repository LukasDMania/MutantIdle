using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class AchievementBaseSO : ScriptableObject, IAchievement
{
    public string AchievementName;
    public string AchievementId;
    public string AchievementDescription;
    public string AchievementRewardText;
    public UnityEvent OnAchievementUnlocked;

    public bool AchievementUnlocked;

    public virtual bool RequirementFullFilled() { return false; }
    public virtual void ApplyAchievement() { }
}

public interface IAchievement
{
    bool RequirementFullFilled();
    void ApplyAchievement();
}
