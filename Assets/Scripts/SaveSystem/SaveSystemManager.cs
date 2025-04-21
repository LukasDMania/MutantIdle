using System;
using System.IO;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.Events;

public class SaveSystemManager : MonoBehaviour
{
    public static SaveSystemManager Instance { get; private set; }

    public DoubleVariable PlayerCurrency;
    public DoubleVariable PrestigeCurrency;
    public DoubleVariable PrestigePointsToAdd;
    public DoubleVariable TickInterval;
    public DoubleVariable TotalTicks;

    public DoubleVariable TotalAfkSeconds;

    public UnityEvent OnGameSaved;
    public UnityEvent OnGameLoaded;

    private string _savePath => Path.Combine(Application.persistentDataPath, "savedata.json");

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Load();
    }

    public void Save()
    {
        SaveData saveData = SetupSaveData();

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

        PlayerCurrency.SetValue(data.PlayerCurrency);
        PrestigeCurrency.SetValue(data.PrestigeCurrency);
        TickInterval.SetValue(data.TickInterval);
        PrestigePointsToAdd.SetValue(data.PrestigePointsToAdd);

        Generator[] generators = FindObjectsByType<Generator>(FindObjectsSortMode.None);
        Debug.Log(generators.Length + " GENERATORSLENGHT");
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

        if (PlayerPrefs.HasKey("LastPlayed"))
        {
            long temp = Convert.ToInt64(PlayerPrefs.GetString("LastPlayed"));
            DateTime lastPlayed = DateTime.FromBinary(temp);
            TimeSpan afkTime = DateTime.Now - lastPlayed;
            TotalAfkSeconds.SetValue(afkTime.TotalSeconds);

            Debug.Log("Time away: " + afkTime.TotalSeconds + " seconds");
        }
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

        Generator[] generators = FindObjectsByType<Generator>(FindObjectsSortMode.None);
        Debug.Log("GENERATORS FETCHED " + generators);

        foreach (var generator in generators)
        {
            GeneratorSaveData genSaveData = generator.Save();
            Debug.Log(genSaveData + " GENSAVADATA");
            saveData.generatorSaveDatas.Add(genSaveData);
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

    private void OnApplicationQuit()
    {
        string timeNow = DateTime.Now.ToBinary().ToString();
        PlayerPrefs.SetString("LastPlayed", timeNow);
        PlayerPrefs.Save();
    }
}

