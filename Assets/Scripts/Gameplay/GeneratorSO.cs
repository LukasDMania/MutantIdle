using UnityEngine;

[CreateAssetMenu(fileName = "NewGenerator", menuName = "Idle Game/Generator")]
public class GeneratorSO : ScriptableObject
{
    [Header("Generator Settings")]

    [Tooltip("Base cost of the generator.")]
    public double BaseCost;

    [Tooltip("Growth rate that increases the cost after each upgrade.")]
    public float GrowthRate;

    [Tooltip("Base production value per time unit.")]
    public double BaseProduction;

}
