using UnityEngine;

public class GlobalMultiplierHandler : MonoBehaviour, IPrestigable
{
    public DoubleVariable GlobalMultiplier;
    public DoubleVariable StartingGlobalMultiplier;
    [SerializeField]
    private bool _ResetGlobalMultiplier;

    public DoubleVariable PrestigeCurrency;

    private MultiplierSystem _multiplierSystem;

    

    private void Awake()
    {
        _multiplierSystem = GetComponent<MultiplierSystem>();
    }

    private void Start()
    {
        if (_ResetGlobalMultiplier) 
        {
            GlobalMultiplier.SetValue(StartingGlobalMultiplier.Value);
        }
    }

    public void CalculateGlobalMultiplier() 
    {
        GlobalMultiplier.SetValue(_multiplierSystem.CalculateTotalMultiplier() + (1 + (PrestigeCurrency.Value / 10)));
        Debug.Log("PrestigeCurrency.Value / 10   " + PrestigeCurrency.Value / 10);
        Debug.Log(GlobalMultiplier.Value);
    }

    public void PrestigeReset()
    {
        _multiplierSystem.PrestigeReset();
        CalculateGlobalMultiplier();
    }
}
