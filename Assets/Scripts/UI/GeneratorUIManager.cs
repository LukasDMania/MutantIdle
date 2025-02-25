using UnityEngine;

public class GeneratorUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _generatorUIPrefab;
    [SerializeField]
    private Transform _generatorContainerTransform;

    public void CreateGeneratorUI(Generator generator) 
    {
        GameObject uiInstance = Instantiate(_generatorUIPrefab, _generatorContainerTransform);
        GeneratorUIElement uiElement = uiInstance.GetComponent<GeneratorUIElement>();
        uiElement.Initialize(generator);
    }
}
