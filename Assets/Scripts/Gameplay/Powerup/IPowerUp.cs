using System;
using UnityEngine;

public interface IPowerUp
{
    void ApplyPowerUp(PowerUpDataHolder dataHolder);
    string TextToDisplayAfterActivation();
}

[Serializable]
public class PowerUpDataHolder 
{
    public DoubleVariable PlayerCurrency;
    public DoubleVariable PrestigePointsToGainOnPrestige;
    public DoubleVariable PrestigeCurrency;
    public DoubleVariable TickInterval;
    public NumberFormatterSO NumberFormatter;
}
