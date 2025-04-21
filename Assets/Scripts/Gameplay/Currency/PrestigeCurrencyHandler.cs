using UnityEngine;

public class PrestigeCurrencyHandler : MonoBehaviour, IPrestigable
{
    public DoubleVariable StartingPrestigeCurrency;
    public DoubleVariable PrestigeCurrency;

    public DoubleVariable StartingPrestigeCurrencyToAddAfterPrestige;
    public DoubleVariable PrestigeCurrencyToAddAfterPrestige;

    [SerializeField] private bool _resetPrestigeCurrency;
    [SerializeField] private bool _resetPrestigeCurrencyToAddAfterPrestige;

    public void Start()
    {
        if (_resetPrestigeCurrency) 
        {
            PrestigeCurrency.SetValue(StartingPrestigeCurrency.Value);
        }
        if (_resetPrestigeCurrencyToAddAfterPrestige)
        {
            PrestigeCurrencyToAddAfterPrestige.SetValue(StartingPrestigeCurrencyToAddAfterPrestige.Value);
        }
    }
    public void PrestigeReset()
    {
        PrestigeCurrency.ApplyChange(PrestigeCurrencyToAddAfterPrestige.Value);
        PrestigeCurrencyToAddAfterPrestige.SetValue(0);
    }
}
