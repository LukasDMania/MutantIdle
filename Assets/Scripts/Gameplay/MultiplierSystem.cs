using System.Collections.Generic;
using UnityEngine;

public class MultiplierSystem : MonoBehaviour 
{
    private MultiplierManager _multiplierManager;


    public Dictionary<int, MultiplierDataSO> activeMultipliers = new Dictionary<int, MultiplierDataSO>();
    public Dictionary<int, MultiplierDataSO> inactiveMultipliers = new Dictionary<int, MultiplierDataSO>();

    private void Start() 
    {
        _multiplierManager = FindFirstObjectByType<MultiplierManager>();
        Debug.Log("MULTIPLIERMAANGEROBJ= " + _multiplierManager);
    }

    public void AddMultiplier(int multiplierId) 
    {
        Debug.Log("MultiplierId: " + multiplierId);
        Debug.Log("_activeMultipliers: " + activeMultipliers);
        if (activeMultipliers.ContainsKey(multiplierId)) { return; }
        
        MultiplierDataSO multiplierDataToAdd = _multiplierManager.GetMultiplierDataSO(multiplierId);
        activeMultipliers.Add(multiplierDataToAdd.MultiplierId, multiplierDataToAdd);
    }

    public void RemoveMultiplier(int multiplierId) 
    {
        
        if (!activeMultipliers.ContainsKey(multiplierId)) { return; }
        inactiveMultipliers.Add(activeMultipliers[multiplierId].MultiplierId, activeMultipliers[multiplierId]);
        activeMultipliers.Remove(multiplierId);

    }

    public double CalculateTotalMultiplier() 
    {
        double totalMultiplier = 0.0;
        double additiveMultiplier = 1;
        double multiplicativeBonus = 1;

        foreach (var multiplier in activeMultipliers)
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
