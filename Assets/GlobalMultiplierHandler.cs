using UnityEngine;

public class GlobalMultiplierHandler : MonoBehaviour
{
    public DoubleVariable GlobalMultiplier;
    public DoubleVariable StartingGlobalMultiplier;
    [SerializeField]
    private bool _ResetGlobalMultiplier;

    private void Start()
    {
        if (_ResetGlobalMultiplier) 
        {
            GlobalMultiplier.SetValue(StartingGlobalMultiplier.Value);
        }
    }
}
