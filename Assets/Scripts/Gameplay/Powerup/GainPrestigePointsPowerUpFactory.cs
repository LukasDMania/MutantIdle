using UnityEngine;

public class GainPrestigePointsPowerUpFactory : IPowerUpFactory
{
    public IPowerUp CreatePowerUp()
    {
        return new GainPrestigePointsPowerUp();
    }
}
