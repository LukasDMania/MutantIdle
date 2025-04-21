using System.Collections.Generic;
using UnityEngine;

public class MultiplierManager : MonoBehaviour
{

    public List<MultiplierDataSO> AllMultipliersList;

    private Dictionary<int, MultiplierDataSO> _allMultipliers = new Dictionary<int, MultiplierDataSO>();

    public bool dictionaryBuilt = false;

    private void Awake()
    {
        BuildMultiplierDictionary();
    }

    public MultiplierDataSO GetMultiplierDataSO(int multiplierId)
    {
        BuildMultiplierDictionary();
        if (!_allMultipliers.ContainsKey(multiplierId)) { return null; }
        Debug.Log(_allMultipliers[multiplierId]);
        return _allMultipliers[multiplierId];
    }

    private void BuildMultiplierDictionary() 
    {
        if (!dictionaryBuilt)
        {
            _allMultipliers.Clear();
            foreach (var multiplier in AllMultipliersList)
            {
                if (!_allMultipliers.ContainsKey(multiplier.MultiplierId))
                {
                    _allMultipliers.Add(multiplier.MultiplierId, multiplier);
                }
                else
                {
                    Debug.Log($"Duplicate found: {multiplier.MultiplierId}, Name: {multiplier.MultiplierName}");
                }
            }
            dictionaryBuilt = true;
        }
    }
}
