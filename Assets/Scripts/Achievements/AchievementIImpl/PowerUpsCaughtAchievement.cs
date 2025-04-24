using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpsCaughtAchievement", menuName = "Achievements/PowerUpsCaughtAchievement")]
public class PowerUpsCaughtAchievement : AchievementBaseSO
{
    public DoubleVariable TotalPowerUpsCaught;
    public DoubleVariable TickInterval;
    public double RequirementAmount;
    public double BuffAmount;


    public override void ApplyAchievement()
    {
        TickInterval.ApplyChange(BuffAmount);
        if (TickInterval.Value <= 0)
        {
            TickInterval.SetValue(0.01);
        }
    }
    public override bool RequirementFullFilled()
    {
        if (RequirementAmount <= TotalPowerUpsCaught.Value)
        {
            return true;
        }
        return false;
    }
}
