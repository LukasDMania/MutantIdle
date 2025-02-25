using DG.Tweening;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public DoubleVariable StartingCurrency;
    public DoubleVariable PlayerCurrency;
    public DoubleVariable GlobalMultiplier;
    public MultiplierSystem MultiplierSystem;
    public bool ResetCurrency;

    private void Start()
    {
        if (ResetCurrency) 
        {
            PlayerCurrency.SetValue(StartingCurrency.Value);
        }
        MultiplierSystem = new MultiplierSystem();
        GlobalMultiplier.Value = MultiplierSystem.CalculateTotalMultiplier();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
}
