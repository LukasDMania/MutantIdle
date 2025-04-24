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
        TickInterval.ApplyChange(IntervalBuffAmount);
        if (TickInterval.Value <= 0)
        {
            TickInterval.SetValue(0.01);
        }
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
