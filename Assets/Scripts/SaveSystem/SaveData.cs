using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public List<GeneratorSaveData> generatorSaveDatas = new List<GeneratorSaveData>();
    public double PlayerCurrency;
    public double PrestigeCurrency;
    public double PrestigePointsToAdd;
    public double TickInterval;
    public double TotalTicks;
}
