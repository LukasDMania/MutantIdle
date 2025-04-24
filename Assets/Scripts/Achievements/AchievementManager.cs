using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : Singleton<AchievementManager>
{
    public List<AchievementBaseSO> AchievementList;

    public AchievementTextDisplayUI AchievementTextDisplayUI;
    public DoubleVariable TotalAchievementsUnlocked;

    private void Start()
    {
    }
    public void TryUnlockingAchievements()
    {
        bool achievementsWereUnlocked = false;
        foreach (var achievement in AchievementList)
        {
            if (!achievement.AchievementUnlocked && achievement.RequirementFullFilled())
            {
                achievement.AchievementUnlocked = true;
                achievementsWereUnlocked = true;
                AchievementTextDisplayUI.DisplayAchievementText(achievement.AchievementRewardText);
                achievement.ApplyAchievement();
                TotalAchievementsUnlocked.ApplyChange(1);
            }
        }
        
        if (achievementsWereUnlocked) 
        {
            TryUnlockingAchievements();
        }
    }

    public List<AchievementsSaveData> Save()
    {
        List<AchievementsSaveData> saveDatas = new List<AchievementsSaveData>();
        foreach (var achievementSO in AchievementList)
        {
            AchievementsSaveData data = new AchievementsSaveData();
            data.AchievementId = achievementSO.AchievementId;
            data.IsUnlocked = achievementSO.AchievementUnlocked;
            saveDatas.Add(data);
        }

        return saveDatas;
    }
    public void Load(List<AchievementsSaveData> saveDatas) 
    {
        foreach (var data in saveDatas)
        {
            foreach (var item in AchievementList)
            {
                if (item.AchievementId == data.AchievementId)
                {
                    item.AchievementUnlocked = data.IsUnlocked;
                }
            }
        }
    }


    public void ResetAllAchievements()
    {
        foreach (var achievement in AchievementList)
        {
            achievement.AchievementUnlocked = false;
        }
    }
}

[Serializable]
public class AchievementsSaveData
{
    public string AchievementId;
    public bool IsUnlocked;
}
