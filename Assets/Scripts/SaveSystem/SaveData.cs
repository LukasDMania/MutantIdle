using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public List<GeneratorSaveData> generatorSaveDatas = new List<GeneratorSaveData>();
    public List<AchievementsSaveData> AchievementSaveDatas = new List<AchievementsSaveData>();
    public GlobalMultiplierSaveData GlobalMultiplierSaveData;
    public double PlayerCurrency;
    public double PrestigeCurrency;
    public double PrestigePointsToAdd;
    public double TickInterval;
    public double TotalTicks;
    public double TotalAchievementsUnlocked;
    public double TotalPrestiges;
    public double TotalGeneratorLevel;
    public double TotalPowerupsClicked;
    public double PrestigeGainMult;
    public double PrestigeGainPercentage;
    public bool GenPP;
    public AudioVolumeSaveData AudioVolumeSaveData;
}
