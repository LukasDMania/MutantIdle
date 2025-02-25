using System.Collections.Generic;
using UnityEngine;

public class MultiplierSystem
{
    public Dictionary<int, MultiplierDataSO> multipliersDictionary = new Dictionary<int, MultiplierDataSO>();

    public void AddMultiplier(MultiplierDataSO multiplierDataSO) 
    {
        if (multipliersDictionary.ContainsKey(multiplierDataSO.MultiplierId)) { return; }
        multipliersDictionary.Add(multiplierDataSO.MultiplierId, multiplierDataSO);
    }

    public void RemoveMultiplier(int multiplierId) 
    {
        if (!multipliersDictionary.ContainsKey(multiplierId)) { return; }
        multipliersDictionary.Remove(multiplierId);
    }

    public double CalculateTotalMultiplier() 
    {
        double totalMultiplier = 0.0;
        double additiveMultiplier = 1;
        double multiplicativeBonus = 1;

        foreach (var multiplier in multipliersDictionary)
        {
            switch (multiplier.Value.MultiplierType)
            {
                case MultiplierType.Additive:
                    additiveMultiplier += multiplier.Value.MultiplierValue;
                    break;
                case MultiplierType.Multiplicative:
                    multiplicativeBonus *= multiplier.Value.MultiplierValue;
                    break;

                default:
                    break;
            }
        }

        totalMultiplier = additiveMultiplier * multiplicativeBonus;
        return totalMultiplier;
    }
}

public enum MultiplierType
{
    Additive,
    Multiplicative,
    MultiplicativeAdditive,
    MultiplicativeMultiply,
}
