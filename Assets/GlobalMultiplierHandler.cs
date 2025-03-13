using UnityEngine;

public class GlobalMultiplierHandler : MonoBehaviour, IPrestigable
{
    public DoubleVariable GlobalMultiplier;
    public DoubleVariable StartingGlobalMultiplier;
    [SerializeField]
    private bool _ResetGlobalMultiplier;

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
    public void PrestigeReset()
    {
        _multiplierSystem.PrestigeReset();
        GlobalMultiplier.SetValue(StartingGlobalMultiplier.Value);
    }
}
