using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VariableDisplayerUI : MonoBehaviour 
{
    [SerializeField]
    private DoubleVariable _variable;
    [SerializeField]
    private TextMeshProUGUI _textToDisplayCurrency;

    public NumberFormatterSO NumberFormatter;

    private void Update()
    {
        _textToDisplayCurrency.text = NumberFormatter.FormatNumber(_variable.Value, NumberFormatterSO.FormatType.Suffix);
    }
}
