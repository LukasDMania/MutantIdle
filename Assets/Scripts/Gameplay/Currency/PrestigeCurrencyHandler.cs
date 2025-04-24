using UnityEngine;

public class PrestigeCurrencyHandler : MonoBehaviour, IPrestigable
{
    public DoubleVariable StartingPrestigeCurrency;
    public DoubleVariable PrestigeCurrency;
    public DoubleVariable PrestigeGainPercentage;
    public DoubleVariable PrestigeGainMult;

    public DoubleVariable StartingPrestigeCurrencyToAddAfterPrestige;
    public DoubleVariable PrestigeCurrencyToAddAfterPrestige;

    [SerializeField] private bool _resetPrestigeCurrency;
    [SerializeField] private bool _resetPrestigeCurrencyToAddAfterPrestige;

    public BoolVariable GeneratePrestigeCurr;

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

    public void GeneratePrestigePoints() 
    {
        if (!GeneratePrestigeCurr.Value) { return; }
        PrestigeCurrency.ApplyChange(PrestigeCurrency.Value * PrestigeGainPercentage.Value * PrestigeGainMult.Value);
    }
}
