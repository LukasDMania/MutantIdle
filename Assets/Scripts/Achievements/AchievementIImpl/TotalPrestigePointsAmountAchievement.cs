using UnityEngine;

[CreateAssetMenu(fileName = "TotalPrestigePointsAmountAchievement", menuName = "Achievements/TotalPrestigePointsAmountAchievement")]
public class TotalPrestigePointsAmountAchievement : AchievementBaseSO
{
    public DoubleVariable PrestigeCurrency;
    public DoubleVariable PrestigeGenerationPercentage;

    public double RequiredPrestigePoints;
    public double BuffToPercentageOfPrestigePointsGenerated;

    public override void ApplyAchievement()
    {
        PrestigeCurrencyHandler p = FindFirstObjectByType<PrestigeCurrencyHandler>();
        p.GeneratePrestigeCurr.SetValue(true);

        PrestigeGenerationPercentage.ApplyChange(BuffToPercentageOfPrestigePointsGenerated);
    }
    public override bool RequirementFullFilled()
    {
        if (RequiredPrestigePoints <= PrestigeCurrency.Value) 
        { 
            return true;
        }
        return false;
    }
}
