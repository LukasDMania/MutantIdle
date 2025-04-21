using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MultiplierSystem : MonoBehaviour, IPrestigable
{
    private MultiplierManager _multiplierManager;


    public Dictionary<int, MultiplierDataSO> activeMultipliers = new Dictionary<int, MultiplierDataSO>();
    public Dictionary<int, MultiplierDataSO> inactiveMultipliers = new Dictionary<int, MultiplierDataSO>();

    private void Start() 
    {
        _multiplierManager = FindFirstObjectByType<MultiplierManager>();
        Debug.Log("MULTIPLIERMAANGEROBJ= " + _multiplierManager);
    }

    public int[] UnlockedMultipliers() 
    {
        var x = activeMultipliers.Values
                .Select(m => m.MultiplierId)
                .ToArray();

        Debug.Log("MULTIPLIER ID TO SAVE IN LENGHT " + activeMultipliers.Count);
        foreach (var item in x)
        {
            Debug.Log("MULTIPLIER IDS TO SAVE IN " + item.ToString());
        }
        return x;
    }

    public void AddMultiplier(int multiplierId) 
    {
        if (_multiplierManager == null)
        {
            _multiplierManager = FindFirstObjectByType<MultiplierManager>();
        }
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

    public void PrestigeReset()
    {
        activeMultipliers?.Clear();
        inactiveMultipliers?.Clear();
    }
}

public enum MultiplierType
{
    Additive,
    Multiplicative,
    MultiplicativeAdditive,
    MultiplicativeMultiply,
}
