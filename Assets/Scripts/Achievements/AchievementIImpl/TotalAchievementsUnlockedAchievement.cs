using UnityEngine;

[CreateAssetMenu(fileName = "TotalAchievementsUnlockedAchievement", menuName = "Achievements/TotalAchievementsUnlockedAchievement")]
public class TotalAchievementsUnlockedAchievement : AchievementBaseSO
{
    public DoubleVariable TotalAchievementsUnlocked;
    public int MultiplierToAdd;
    public double RequirementAmount;

    public override void ApplyAchievement()
    {
        GlobalMultiplierHandler g = FindAnyObjectByType<GlobalMultiplierHandler>();
        g.AddMultiplier(MultiplierToAdd);
    }
    public override bool RequirementFullFilled()
    {
        if (RequirementAmount <= TotalAchievementsUnlocked.Value)
        {
            return true;
        }
        return false;
    }
}
