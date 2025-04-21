using Unity.VisualScripting;
using UnityEngine;

public class GainPrestigePointsPowerUp : IPowerUp
{
    private PowerUpDataHolder powerUpDataHolder;
    private double addedPrestigePoints;
    public void ApplyPowerUp(PowerUpDataHolder dataHolder)
    {
        powerUpDataHolder = dataHolder;
        powerUpDataHolder.PrestigePointsToGainOnPrestige = dataHolder.PrestigePointsToGainOnPrestige;
        powerUpDataHolder.PrestigeCurrency = dataHolder.PrestigeCurrency;
        powerUpDataHolder.NumberFormatter = dataHolder.NumberFormatter;

        double prestigePointsToAdd = powerUpDataHolder.PrestigeCurrency.Value * 0.1;
        if (prestigePointsToAdd == 0)
        {
            prestigePointsToAdd = 1;
        }
        addedPrestigePoints = prestigePointsToAdd;
        powerUpDataHolder.PrestigePointsToGainOnPrestige.ApplyChange(prestigePointsToAdd);
    }

    public string TextToDisplayAfterActivation()
    {
        if (addedPrestigePoints == 1)
        {       
            return $"Gained {powerUpDataHolder.NumberFormatter.FormatNumber(addedPrestigePoints, NumberFormatterSO.FormatType.Suffix)} EVO Point";
        }
        return $"Gained {powerUpDataHolder.NumberFormatter.FormatNumber(addedPrestigePoints, NumberFormatterSO.FormatType.Suffix)} EVO Points";
    }
}
