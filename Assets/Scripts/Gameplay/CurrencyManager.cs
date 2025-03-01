using DG.Tweening;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
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

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
}
