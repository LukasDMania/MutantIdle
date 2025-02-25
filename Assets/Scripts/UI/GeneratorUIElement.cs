using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorUIElement : MonoBehaviour
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


    private Generator _linkedGenerator;

    public void Initialize(Generator generatorToLink)
    {
        _linkedGenerator = generatorToLink;
        _upgradeButton.onClick.AddListener(OnUgradeClicked);
        UpdateUI();
    }

    private void UpdateUI()
    {
        _bodyPartSprite.sprite = _linkedGenerator.BodyPartDataSO.GetSpriteForLevel(_linkedGenerator.GeneratorLevel);
        _generatorLevel.text = $"{_linkedGenerator.GeneratorLevel}";
        _slider.value = _linkedGenerator.CalculatePercentageToNextMultiplier();
        _productionRate.text = $"{_linkedGenerator.TotalProduction:F2}/t";
        //_linkedGenerator.CalculateTotalProduction();
        _generatorMultiplier.text = $"Multiplier: {_linkedGenerator.TotalMultiplier:F2}x";
        _upgradeButtonText.text = $"{_linkedGenerator.CurrentUpgradeCost:F2}";
    }

    private void OnUgradeClicked()
    {
        _linkedGenerator.UpgradeGenerator();
        UpdateUI();
    }
}
