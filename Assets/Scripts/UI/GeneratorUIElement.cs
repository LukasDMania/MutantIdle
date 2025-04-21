using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GeneratorUIElement : MonoBehaviour, IPrestigable
{
    [Header("UI References")]
    [SerializeField]
    private TextMeshProUGUI _generatorLevel;
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private TextMeshProUGUI _generatorMultiplier;
    [SerializeField]
    private TextMeshProUGUI _productionRate;
    [SerializeField]
    private Button _upgradeButton;
    [SerializeField]
    private TextMeshProUGUI _upgradeButtonText;
    [SerializeField]
    private Image _bodyPartSprite;
    public NumberFormatterSO NumberFormatter;


    private Generator _linkedGenerator;

    public void Initialize(Generator generatorToLink)
    {
        _linkedGenerator = generatorToLink;
        _upgradeButton.onClick.AddListener(OnUpgradeClicked);
        UpdateUI();
    }

    private void UpdateUI()
    {
        Debug.Log("LINKEDGEN LEVEL " + _linkedGenerator.GeneratorLevel);
        _bodyPartSprite.sprite = _linkedGenerator.BodyPartDataSO.GetSpriteForLevel(_linkedGenerator.GeneratorLevel);
        _generatorLevel.text = $"{_linkedGenerator.GeneratorLevel}";
        _slider.value = _linkedGenerator.CalculatePercentageToNextMultiplier();
        _productionRate.text = $"{NumberFormatter.FormatNumber(_linkedGenerator.TotalProductionWithoutGlobal, NumberFormatterSO.FormatType.Suffix)}/t";
        //_linkedGenerator.CalculateTotalProduction();
        _generatorMultiplier.text = $"Multiplier: {NumberFormatter.FormatNumber(_linkedGenerator.TotalMultiplierWithoutGlobal, NumberFormatterSO.FormatType.Suffix)}x";
        _upgradeButtonText.text = $"{NumberFormatter.FormatNumber(_linkedGenerator.CurrentUpgradeCost, NumberFormatterSO.FormatType.Suffix)}";
    }

    private void OnUpgradeClicked()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            Debug.Log("Control key held down during upgrade.");
            _linkedGenerator.UpgradeGeneratorMax();
        }

        _linkedGenerator.UpgradeGenerator();
        UpdateUI();
    }


    public void PrestigeReset()
    {
        UpdateUI();
    }
}
