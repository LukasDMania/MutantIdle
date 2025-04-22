using UnityEngine;

[CreateAssetMenu(fileName = "PrestigeAmountAchievement", menuName = "Achievements/PrestigeAmountAchievement")]
public class PrestigeAmountAchievement : AchievementBaseSO
{
    public DoubleVariable TotalTimesPrestiged;
    public DoubleVariable PrestigeMultiplierGain;

    public double RequiredPrestiges;
    public double BuffAmount;
    public override void ApplyAchievement()
    {
        PrestigeMultiplierGain.ApplyChange(BuffAmount);
    }
    public override bool RequirementFullFilled()
    {
        if (RequiredPrestiges <= TotalTimesPrestiged.Value)
        {
            return true;
        }
        return false;
    }
}
