using System;
using System.IO;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SaveSystemManager : MonoBehaviour
{
    public static SaveSystemManager Instance { get; private set; }

    public DoubleVariable PlayerCurrency;
    public DoubleVariable PrestigeCurrency;
    public DoubleVariable PrestigePointsToAdd;
    public DoubleVariable TickInterval;
    public DoubleVariable TotalTicks;
    public DoubleVariable TotalAfkSeconds;
    public DoubleVariable TotalAchievementsUnlocked;
    public DoubleVariable TotalPrestiges;
    public DoubleVariable TotalGeneratorLevel;
    public DoubleVariable TotalPowerupsClicked;
    public DoubleVariable PrestigeGainMult;
    public DoubleVariable PrestigeGainPercentage;
    public BoolVariable GeneratePrestigePoints;

    public UnityEvent OnGameSaved;
    public UnityEvent OnGameLoaded;
    public UnityEvent OnGameReset;

    private string _savePath => Path.Combine(Application.persistentDataPath, "savedata.json");

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        //ResetMainVars();
        Instance = this;
        if (PlayerPrefs.HasKey("LastPlayed"))
        {
            long temp = Convert.ToInt64(PlayerPrefs.GetString("LastPlayed"));
            DateTime lastPlayed = DateTime.FromBinary(temp);
            TimeSpan afkTime = DateTime.Now - lastPlayed;
            TotalAfkSeconds.SetValue(afkTime.TotalSeconds);

            Debug.Log("Time away: " + afkTime.TotalSeconds + " seconds");
        }
    }
    private void Start()
    {
        Load();
    }

    public void Save()
    {
        SaveData saveData = SetupSaveData();
        Debug.Log(saveData.AchievementSaveDatas[0].AchievementId);
        string json = JsonUtility.ToJson(saveData, true);
        Debug.Log("Saving: " + json);
        File.WriteAllText(_savePath, json);
        Debug.Log("Saved to: " + _savePath);
        OnGameSaved?.Invoke();
    }
    public void Load()
    {
        if (!File.Exists(_savePath)) { return; }

        string json = File.ReadAllText(_savePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        Debug.Log("Loaded: " + json);

        //PlayerCurrency.SetValue(data.PlayerCurrency);
        PrestigeCurrency.SetValue(data.PrestigeCurrency);
        TickInterval.SetValue(data.TickInterval);
        PrestigePointsToAdd.SetValue(data.PrestigePointsToAdd);
        TotalTicks.SetValue(data.TotalTicks);
        TotalAchievementsUnlocked.SetValue(data.TotalAchievementsUnlocked);
        TotalPrestiges.SetValue(data.TotalPrestiges);
        TotalGeneratorLevel.SetValue(data.TotalGeneratorLevel);
        TotalPowerupsClicked.SetValue(data.TotalPowerupsClicked);
        PrestigeGainMult.SetValue(data.PrestigeGainMult);
        PrestigeGainPercentage.SetValue(data.PrestigeGainPercentage);
        GeneratePrestigePoints.SetValue(data.GenPP);

        GlobalMultiplierHandler h = FindFirstObjectByType<GlobalMultiplierHandler>();
        h.Load(data.GlobalMultiplierSaveData);

        Generator[] generators = FindObjectsByType<Generator>(FindObjectsSortMode.None);
        foreach (var generatorSaveData in data.generatorSaveDatas)
        {
            foreach (var generator in generators)
            {
                if (generator.gameObject.name == generatorSaveData.GeneratorName)
                {
                    generator.Load(generatorSaveData);
                    break;
                }
            }
        }

        AchievementManager.Instance.Load(data.AchievementSaveDatas);

        OnGameLoaded?.Invoke();
    }



    private SaveData SetupSaveData()
    {
        SaveData saveData = new SaveData();
        saveData.PlayerCurrency = PlayerCurrency.Value;
        saveData.TickInterval = TickInterval.Value;
        saveData.PrestigeCurrency = PrestigeCurrency.Value;
        saveData.PrestigePointsToAdd = PrestigePointsToAdd.Value;
        saveData.TotalTicks = TotalTicks.Value;
        saveData.TotalAchievementsUnlocked = TotalAchievementsUnlocked.Value;
        saveData.TotalPrestiges = TotalPrestiges.Value;
        saveData.TotalGeneratorLevel = TotalGeneratorLevel.Value;
        saveData.TotalPowerupsClicked = TotalPowerupsClicked.Value;
        saveData.PrestigeGainMult = PrestigeGainMult.Value;
        saveData.PrestigeGainPercentage = PrestigeGainPercentage.Value;
        saveData.GenPP = GeneratePrestigePoints.Value;

        GlobalMultiplierHandler h = FindFirstObjectByType<GlobalMultiplierHandler>();
        saveData.GlobalMultiplierSaveData = h.Save();
        Generator[] generators = FindObjectsByType<Generator>(FindObjectsSortMode.None);

        foreach (var generator in generators)
        {
            GeneratorSaveData genSaveData = generator.Save();
            saveData.generatorSaveDatas.Add(genSaveData);
        }

        saveData.AchievementSaveDatas = AchievementManager.Instance.Save();
        Debug.Log(saveData.AchievementSaveDatas.Count);
        foreach (var item in saveData.AchievementSaveDatas)
        {
            Debug.Log(item.AchievementId + " " + item.IsUnlocked);
        }

        return saveData;
    }

    public void DeleteSave()
    {
        if (File.Exists(_savePath))
        {
            File.Delete(_savePath);
            Debug.Log("Save file deleted.");
        }
    }
    public void DeleteSaveAndResetGame()
    {
        DeleteSave();
        ResetMainVars();
        ResetGameData();
        Save();
        OnGameReset?.Invoke();
    }

    private void ResetGameData()
    {
        AchievementManager.Instance.ResetAllAchievements();
        Generator[] generators = FindObjectsByType<Generator>(FindObjectsSortMode.None);
        foreach (var generator in generators)
        {
            Debug.Log(generator.GeneratorLevel);
            generator.ResetGenerator();
            Debug.Log(generator.GeneratorLevel);
        }
    }
    private void ResetMainVars()
    {
        PlayerCurrency.SetValue(5);
        TickInterval.SetValue(1);
        PrestigeCurrency.SetValue(0);
        PrestigePointsToAdd.SetValue(0);
        TotalTicks.SetValue(0);
        TotalAchievementsUnlocked.SetValue(0);
        TotalPrestiges.SetValue(0);
        TotalPowerupsClicked.SetValue(0);
        TotalGeneratorLevel.SetValue(0);
        PrestigeGainMult.SetValue(1);
        PrestigeGainPercentage.SetValue(0);
        GeneratePrestigePoints.SetValue(false);
    }

    private void OnApplicationQuit()
    {
        string timeNow = DateTime.Now.ToBinary().ToString();
        PlayerPrefs.SetString("LastPlayed", timeNow);
        PlayerPrefs.Save();
        Save();
    }

    public void ResetVarsForPrestige() 
    {
        TotalGeneratorLevel.SetValue(0);
    }
}

