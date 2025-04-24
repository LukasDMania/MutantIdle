using System;
using UnityEngine;

public class GlobalMultiplierHandler : MonoBehaviour, IPrestigable
{
    public DoubleVariable GlobalMultiplier;
    public DoubleVariable StartingGlobalMultiplier;
    [SerializeField]
    private bool _ResetGlobalMultiplier;

    public DoubleVariable PrestigeCurrency;

    private MultiplierSystem _multiplierSystem;

    

    private void Awake()
    {
        _multiplierSystem = GetComponent<MultiplierSystem>();
    }

    private void Start()
    {
        if (_ResetGlobalMultiplier) 
        {
            GlobalMultiplier.SetValue(StartingGlobalMultiplier.Value);
        }
        CalculateGlobalMultiplier();
    }

    public void CalculateGlobalMultiplier() 
    {
        if (PrestigeCurrency.Value > 0)
        {
            GlobalMultiplier.SetValue(_multiplierSystem.CalculateTotalMultiplier() + (PrestigeCurrency.Value / 10));

        }
        else
        {
            GlobalMultiplier.SetValue(_multiplierSystem.CalculateTotalMultiplier());
        }
    }

    public void AddMultiplier(int id) 
    {
        _multiplierSystem.AddMultiplier(id);
    }

    public void PrestigeReset()
    {
        _multiplierSystem.PrestigeReset();
        CalculateGlobalMultiplier();
    }

    public GlobalMultiplierSaveData Save()
    {
        GlobalMultiplierSaveData h = new GlobalMultiplierSaveData();
        h.UnlockedMultipliers = _multiplierSystem.UnlockedMultipliers();
        return h;
    }

    public void Load(GlobalMultiplierSaveData saveData)
    {
        foreach (var upgradeId in saveData.UnlockedMultipliers)
        {

            if (_multiplierSystem == null)
            {
                _multiplierSystem = GetComponent<MultiplierSystem>();
            }
            _multiplierSystem.AddMultiplier(upgradeId);
        }
    }
}

[Serializable]
public class GlobalMultiplierSaveData
{
    public int[] UnlockedMultipliers;
}
