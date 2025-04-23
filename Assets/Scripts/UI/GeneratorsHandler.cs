using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GeneratorsHandler : MonoBehaviour, IPrestigable
{
    [SerializeField]
    private List<Generator> _generators;
    [SerializeField]
    private GeneratorUIManager _generatorUIManager;
    [SerializeField]
    private DoubleVariable _TotalCurrencyGeneration;

    public DoubleVariable PlayerCurrency;
    public DoubleVariable TickInterval;
    public DoubleVariable AfkSeconds;
    public DoubleVariable AfkGenerationPostCalculation;

    public UnityEvent OnAfkGenerationCalculated;

    private void Start()
    {
        _generators = new List<Generator>(GetComponentsInChildren<Generator>());
        PopulateGeneratorsInUI();

        CalculateTotalGeneratorProduction();
    }

    private void PopulateGeneratorsInUI()
    {
        foreach (var generator in _generators)
        {
            _generatorUIManager.CreateGeneratorUI(generator);
        }
    }

    public void CalculateTotalGeneratorProduction() 
    {
        double totalProduction = 0;
        foreach (var generator in _generators)
        {
            if (generator.GeneratorLevel != 0)
            {
                generator.CalculateTotalProduction();
                totalProduction += generator.TotalProduction;
            }
        }
        _TotalCurrencyGeneration.SetValue(totalProduction);
    }

    public void PrestigeReset()
    {
        CalculateTotalGeneratorProduction();
    }
    public void CalculateAfkCurrencyGenerated() 
    {
        if (_generators == null) { _generators = new List<Generator>(GetComponentsInChildren<Generator>()); }
        Debug.Log("CALCULATEAFKCURR _TotalCurrencyGen" + _TotalCurrencyGeneration.Value);
        CalculateTotalGeneratorProduction();
        Debug.Log("CALCULATEAFKCURR _TotalCurrencyGen POST" + _TotalCurrencyGeneration.Value);
        double afkGeneration = AfkSeconds.Value / TickInterval.Value * _TotalCurrencyGeneration.Value;
        Debug.Log(afkGeneration);
        AfkGenerationPostCalculation.SetValue(afkGeneration);
        OnAfkGenerationCalculated?.Invoke();
        Debug.Log(PlayerCurrency.Value);
        PlayerCurrency.ApplyChange(afkGeneration);
        Debug.Log(PlayerCurrency.Value);
    }
}
