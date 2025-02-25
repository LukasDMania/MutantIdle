using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorInitializer : MonoBehaviour
{
    [SerializeField]
    private List<Generator> _generators;
    [SerializeField]
    private GeneratorUIManager _generatorUIManager;

    private void PopulateGeneratorsInUI() 
    {
        foreach (var generator in _generators)
        {
            _generatorUIManager.CreateGeneratorUI(generator);
        }
    }

    private void Start()
    {
        PopulateGeneratorsInUI();
    }
}
