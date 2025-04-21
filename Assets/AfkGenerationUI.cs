using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class AfkGenerationUI : MonoBehaviour
{
    public GameObject Container;
    public DoubleVariable AfkCurrencyGenerated;
    public NumberFormatterSO NumberFormatterSO;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void ShowAfkGainContainer() 
    {
        Container.SetActive(true);
        TextMeshProUGUI textMeshProUGUI = Container.GetComponentInChildren<TextMeshProUGUI>();

        textMeshProUGUI.text = $"You've earned {NumberFormatterSO.FormatNumber(AfkCurrencyGenerated.Value, NumberFormatterSO.FormatType.Suffix)} DNA While you were away";
    }

    public void HideAfkGainContainer() 
    {
        Container.SetActive(false);
    }
}
