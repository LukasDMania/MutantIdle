using UnityEngine;

public class ProductionBoostPowerUpFactory : IPowerUpFactory
{
    public IPowerUp CreatePowerUp()
    {
        return new ProductionBoostPowerUp();
    }
}
