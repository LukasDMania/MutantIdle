using UnityEngine;

[CreateAssetMenu(fileName = "TotalTicksAchievement", menuName = "Achievements/TotalTicksAchievement")]
public class TotalTicksAchievement : AchievementBaseSO
{
    public DoubleVariable TotalTicks;
    public DoubleVariable TickInterval;
    public double IntervalBuffAmount;

    public double RequiredTicks;

    public override void ApplyAchievement()
    {
        PrestigeCurrencyHandler p = FindFirstObjectByType<PrestigeCurrencyHandler>();
    }
    public override bool RequirementFullFilled()
    {
        if (RequiredTicks <= TotalTicks.Value)
        {
            return true;
        }
        return false;
    }
}
