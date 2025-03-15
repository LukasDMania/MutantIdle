using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorsHandler : MonoBehaviour
{
    [SerializeField]
    private List<Generator> _generators;
    [SerializeField]
    private GeneratorUIManager _generatorUIManager;
    [SerializeField]
    private DoubleVariable _TotalCurrencyGeneration;

    private void Start()
    {
        _generators = new List<Generator>(GetComponentsInChildren<Generator>());
        PopulateGeneratorsInUI();
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
}
