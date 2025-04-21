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


    [SerializeField]
    private DoubleVariable _playerCurrency;

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
        Debug.Log("BASEPRODUCTION" + GeneratorSO.BaseProduction);
        Debug.Log("MULTIPLIER" + TotalMultiplier);
    }

    public void CalculateTotalProduction()
    {
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

    public void UpgradeGenerator() 
    {
        Debug.Log("Upgrading generator attempting");
        if (CurrentUpgradeCost > _playerCurrency.Value)
        {
            Debug.Log($"Can't upgrade: cost {CurrentUpgradeCost}, currency {_playerCurrency.Value}");
            return;
        }

        _playerCurrency.ApplyChange(-CurrentUpgradeCost);
        GeneratorLevel++;

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
            PrestigePointsToAddAfterPrestige.ApplyChange(1);
        }

        CalculateTotalProduction();
        CalculateUpgradeCost();
        OnGeneratorUpgrade?.Invoke();
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

        Debug.Log($"GeneratorLevel: {GeneratorLevel}");
        Debug.Log($"PreviousUpgradeLevel: {previousUpgradeLevel}");
        Debug.Log($"NextUpgradeLevel: {nextUpgradeLevel}");
        Debug.Log($"Progress: {progress}");

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
}
