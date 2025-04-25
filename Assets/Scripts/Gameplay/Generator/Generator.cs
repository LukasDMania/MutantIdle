using DG.Tweening;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Generator : MonoBehaviour, ITickable, IPrestigable
{
    public GeneratorSO GeneratorSO;
    public BodyPartDataSO BodyPartDataSO;
    [SerializeField]
    private MultiplierSystem _multiplierSystem;
    public DoubleVariable GlobalMultiplier;
    public DoubleVariable PrestigePointsToAddAfterPrestige;
    public DoubleVariable PrestigePointGainMultiplier;


    [SerializeField]
    private DoubleVariable _playerCurrency;
    public DoubleVariable TotalGeneratorLevel;

    public int GeneratorLevel = 0;
    public double TotalProduction;
    public double TotalProductionWithoutGlobal;
    public double TotalMultiplier;
    public double TotalMultiplierWithoutGlobal;
    public double CurrentUpgradeCost;

    public CharacterVisualManager CharacterVisualManager;

    public UnityEvent OnGeneratorUpgrade;

    private void Awake()
    {
        _multiplierSystem = GetComponent<MultiplierSystem>();
        CalculateUpgradeCost();
        CalculateTotalProduction();
    }

    private void Start()
    {
        CharacterVisualManager = FindFirstObjectByType<CharacterVisualManager>();
    }

    public void CalculateTotalProduction()
    {
        if (_multiplierSystem == null) { _multiplierSystem = GetComponent<MultiplierSystem>(); };
        TotalMultiplierWithoutGlobal = _multiplierSystem.CalculateTotalMultiplier();
        TotalMultiplier = _multiplierSystem.CalculateTotalMultiplier() * GlobalMultiplier.Value;
        TotalProduction = (GeneratorSO.BaseProduction * GeneratorLevel) * TotalMultiplier;
        TotalProductionWithoutGlobal = (GeneratorSO.BaseProduction * GeneratorLevel) * TotalMultiplierWithoutGlobal;
    }

    private void CalculateUpgradeCost() 
    {
        if (GeneratorLevel == 0)
        {
            CurrentUpgradeCost = GeneratorSO.BaseCost;
        }
        else
        {
            CurrentUpgradeCost = GeneratorSO.BaseCost * Mathf.Pow(GeneratorSO.GrowthRate, GeneratorLevel);
        }
    }
    private bool CanUpgrade() 
    {
        Debug.Log("Upgrading generator attempting");
        if (CurrentUpgradeCost > _playerCurrency.Value)
        {
            Debug.Log($"Can't upgrade: cost {CurrentUpgradeCost}, currency {_playerCurrency.Value}");
            return false;
        }
        return true;
    }
    public void UpgradeGenerator() 
    {
        if (!CanUpgrade()) { return; }
        //AudioManager.Instance.AudioSystemSO.Play(SoundName.ButtonClick_1);
        _playerCurrency.ApplyChange(-CurrentUpgradeCost);
        GeneratorLevel++;
        TotalGeneratorLevel.ApplyChange(1);

        // Handle visuals
        if (GeneratorLevel == 1)
        {
            Debug.Log("Adding initial Body Part");
            CharacterVisualManager.AddBodyPart(this, BodyPartDataSO);
        }
        else
        {
            Debug.Log($"Updating Body Part sprite for level {GeneratorLevel}");
            CharacterVisualManager.UpdateBodyPartSprite(this, BodyPartDataSO);
        }

        //Handle multiplier for level intervals
        if (GeneratorLevel == 25) 
        {
            _multiplierSystem.AddMultiplier(0);

        }
        if (GeneratorLevel == 50)
        {
            _multiplierSystem.AddMultiplier(1);
        }
        if (GeneratorLevel == 100)
        {
            _multiplierSystem.AddMultiplier(2);
        }
        if (GeneratorLevel % 100 == 0 && GeneratorLevel <= 5000)
        {
            PrestigePointsToAddAfterPrestige.ApplyChange((GeneratorLevel / 100) * PrestigePointGainMultiplier.Value);
        }


        CalculateTotalProduction();
        CalculateUpgradeCost();
        OnGeneratorUpgrade?.Invoke();
    }
    public void UpgradeGeneratorMax() 
    {
        while (CanUpgrade())
        {
            UpgradeGenerator();
        }
    }

    public void AddMultiplier(int id) 
    {
        _multiplierSystem.AddMultiplier(id);
    }
    public void OnTick()
    {
        _playerCurrency.ApplyChange(TotalProduction);
    }

    public float CalculatePercentageToNextMultiplier()
    {
        int previousUpgradeLevel = BodyPartDataSO.GetPreviousUpgradeLevel(GeneratorLevel);
        int nextUpgradeLevel = BodyPartDataSO.GetNextUpgradeLevelForLevel(GeneratorLevel + 1);
        if (nextUpgradeLevel == -1)
        {
            return 1;
        }
        if (nextUpgradeLevel <= previousUpgradeLevel)
        {
            Debug.LogWarning("Next upgrade level is not higher than the previous upgrade level.");
            return 0;
        }

        float progress = (float)(GeneratorLevel - previousUpgradeLevel) / (nextUpgradeLevel - previousUpgradeLevel);

        // Clamp the result between 0 and 1 (valid percentage for sliders)
        return Mathf.Clamp01(progress);
    }

    public void PrestigeReset()
    {
        _multiplierSystem.PrestigeReset();
        GeneratorLevel = 0;
        CalculateUpgradeCost();
        CalculateTotalProduction();
    }

    public void ResetGenerator() 
    {
        GeneratorLevel = 0;
        _multiplierSystem.ResetMultiplierSystem();
        CalculateUpgradeCost();
        CalculateTotalProduction();
    }

    public GeneratorSaveData Save()
    {
        return new GeneratorSaveData
        {
            GeneratorName = gameObject.name,
            GeneratorLevelToSave = GeneratorLevel,
            UnlockedMultipliers = _multiplierSystem.UnlockedMultipliers()
        };
    }

    public void Load(GeneratorSaveData saveData)
    {
        GeneratorLevel = saveData.GeneratorLevelToSave;
        foreach (var upgradeId in saveData.UnlockedMultipliers)
        {

            if (_multiplierSystem == null)
            {
                _multiplierSystem = GetComponent<MultiplierSystem>();
            }
            _multiplierSystem.AddMultiplier(upgradeId);
        }
        CalculateUpgradeCost();
        CalculatePercentageToNextMultiplier();
        CalculateTotalProduction();
        UpdateSpriteUponLoad();
    }
    private void UpdateSpriteUponLoad() 
    {
        if (GeneratorLevel >= 1)
        {
            if (CharacterVisualManager == null) { CharacterVisualManager = FindFirstObjectByType<CharacterVisualManager>(); }
            CharacterVisualManager.AddBodyPart(this, BodyPartDataSO);
        }
        if (GeneratorLevel >= 2)
        {
            CharacterVisualManager.UpdateBodyPartSprite(this, BodyPartDataSO);
        }
    }
}


[Serializable]
public class GeneratorSaveData
{
    public string GeneratorName;
    public int GeneratorLevelToSave;
    public int[] UnlockedMultipliers;
}
