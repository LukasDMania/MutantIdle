using UnityEngine;


[CreateAssetMenu(fileName = "TotalGeneratorLevel", menuName = "Achievements/TotalGeneratorLevel")]
public class TotalGeneratorLevelAchievement : AchievementBaseSO
{
    public DoubleVariable TotalGeneratorLevel;
    public double LevelRequirement;

    public int UpgradeIdToApply;

    public override void ApplyAchievement()
    {
        Generator[] gens = FindObjectsByType<Generator>(FindObjectsSortMode.None);
        foreach (Generator g in gens) 
        {
            g.AddMultiplier(UpgradeIdToApply);
        }

    }
    public override bool RequirementFullFilled()
    {
        if (LevelRequirement <= TotalGeneratorLevel.Value )
        {
            return true;
        }
        return false;
    }
}
