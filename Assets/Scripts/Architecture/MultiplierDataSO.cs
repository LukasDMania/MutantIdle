using UnityEngine;

[CreateAssetMenu(
    fileName = "NewMultiplierData",
    menuName = "Game Data/Multiplier",
    order = 1)]
public class MultiplierDataSO : ScriptableObject
{
    public int MultiplierId;
    public string MultiplierName;
    public string MultiplierDescription;
    public MultiplierType MultiplierType;
    public float MultiplierDuration;
    public double MultiplierValue;
    public bool IsPermanent = false;
}