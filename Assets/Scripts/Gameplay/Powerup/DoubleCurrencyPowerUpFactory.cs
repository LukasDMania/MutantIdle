using UnityEngine;

public class DoubleCurrencyPowerUpFactory : IPowerUpFactory
{
    public IPowerUp CreatePowerUp()
    {
        return new DoubleCurrencyPowerUp();
    }
}
