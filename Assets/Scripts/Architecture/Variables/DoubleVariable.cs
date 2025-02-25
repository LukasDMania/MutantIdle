using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Double")]
public class DoubleVariable : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif
    public double Value;

    public void SetValue(double value)
    {
        Value = value;
    }

    public void SetValue(DoubleVariable value)
    {
        Value = value.Value;
    }

    public void ApplyChange(double amount)
    {
        Value += amount;
    }

    public void ApplyChange(DoubleVariable amount)
    {
        Value += amount.Value;
    }
}
