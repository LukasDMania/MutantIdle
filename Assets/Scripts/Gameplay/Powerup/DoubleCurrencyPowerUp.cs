using UnityEngine;

public class DoubleCurrencyPowerUp : IPowerUp
{
    public void ApplyPowerUp(PowerUpDataHolder dataHolder)
    {
        double playerCurrency = dataHolder.PlayerCurrency.Value;
        dataHolder.PlayerCurrency.ApplyChange(playerCurrency);
    }

    public string TextToDisplayAfterActivation()
    {
        return "DNA Doubled!";
    }
}
