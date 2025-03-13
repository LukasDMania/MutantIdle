using DG.Tweening;
using UnityEngine;

public class CurrencyManager : MonoBehaviour, IPrestigable
{
    public DoubleVariable StartingCurrency;
    public DoubleVariable PlayerCurrency;
    public bool ResetCurrency;

    private void Start()
    {
        if (ResetCurrency) 
        {
            PlayerCurrency.SetValue(StartingCurrency.Value);
        }
    }

    public void PrestigeReset()
    {
        PlayerCurrency.SetValue(StartingCurrency.Value);
    }
}
