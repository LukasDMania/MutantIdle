using System;
using UnityEngine;

public class Generator : MonoBehaviour, ITickable
{
    public GeneratorSO GeneratorSO;
    public BodyPartDataSO BodyPartDataSO;


    [SerializeField]
    private DoubleVariable _playerCurrency;

    public int GeneratorLevel = 0;
    public double TotalProduction;
    public double CurrentUpgradeCost;

    public CharacterVisualManager CharacterVisualManager;

    private void Awake()
    {
        CalculateUpgradeCost();
        CalculateTotalProduction();
    }

    private void Start()
    {
        CharacterVisualManager = FindFirstObjectByType<CharacterVisualManager>();
    }

    private void CalculateTotalProduction()
    {
        // Add multipliers to the end here
        TotalProduction = GeneratorSO.BaseProduction * GeneratorLevel;
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

        CalculateTotalProduction();
        CalculateUpgradeCost();

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
    }


    public void OnTick()
    {
        _playerCurrency.ApplyChange(TotalProduction);
    }

    public float CalculatePercentageToNextMultiplier()
    {
        int previousUpgradeLevel = BodyPartDataSO.GetPreviousUpgradeLevel(GeneratorLevel);
        int nextUpgradeLevel = BodyPartDataSO.GetNextUpgradeLevelForLevel(GeneratorLevel + 1);

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
