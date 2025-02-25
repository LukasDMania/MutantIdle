using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Generator : MonoBehaviour, ITickable
{
    public GeneratorSO GeneratorSO;
    public BodyPartDataSO BodyPartDataSO;
    public MultiplierSystem MultiplierSystem;
    public List<MultiplierDataSO> LevelUpMultipliers;
    public DoubleVariable GlobalMultiplier;


    [SerializeField]
    private DoubleVariable _playerCurrency;

    public int GeneratorLevel = 0;
    public double TotalProduction;
    public double TotalMultiplier;
    public double CurrentUpgradeCost;

    public CharacterVisualManager CharacterVisualManager;

    private void Awake()
    {
        MultiplierSystem = new MultiplierSystem();
        CalculateUpgradeCost();
        CalculateTotalProduction();
    }

    private void Start()
    {
        CharacterVisualManager = FindFirstObjectByType<CharacterVisualManager>();
    }

    public void CalculateTotalProduction()
    {
        TotalMultiplier = MultiplierSystem.CalculateTotalMultiplier() * GlobalMultiplier.Value;
        TotalProduction = (GeneratorSO.BaseProduction * GeneratorLevel) * TotalMultiplier;
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
            MultiplierSystem.AddMultiplier(LevelUpMultipliers[0]);
        }
        if (GeneratorLevel == 50)
        {
            MultiplierSystem.AddMultiplier(LevelUpMultipliers[1]);
        }
        if (GeneratorLevel == 100)
        {
            MultiplierSystem.AddMultiplier(LevelUpMultipliers[2]);
        }

        CalculateTotalProduction();
        CalculateUpgradeCost();
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
}
