using UnityEngine;

public class PrestigeCurrencyHandler : MonoBehaviour, IPrestigable
{
    public DoubleVariable StartingPrestigeCurrency;
    public DoubleVariable PrestigeCurrency;

    public DoubleVariable PrestigeCurrencyToAddAfterPrestige;

    [SerializeField] private bool _resetPrestigeCurrency;

    public void Start()
    {
        if (_resetPrestigeCurrency) 
        {
            PrestigeCurrency.SetValue(StartingPrestigeCurrency.Value);
        }
    }
    public void PrestigeReset()
    {
        PrestigeCurrency.ApplyChange(PrestigeCurrencyToAddAfterPrestige.Value);
        PrestigeCurrencyToAddAfterPrestige.SetValue(0);
    }
}
