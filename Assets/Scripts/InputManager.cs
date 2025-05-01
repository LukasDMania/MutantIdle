using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InputManager : MonoBehaviour
{
    public AchievementContainerUIScript AchievementUI;
    public StatsScreenContainerUI StatsUI;
    private PrestigeButtonHandler p;

    private Dictionary<string, Generator> generatorDictionary;
    private void Start()
    {
        Generator[] genArray = FindObjectsByType<Generator>(FindObjectsSortMode.InstanceID);
        if (genArray != null)
        {
            generatorDictionary = new Dictionary<string, Generator>();
            generatorDictionary.Clear();
            foreach (var item in genArray)
            {
                if (!generatorDictionary.ContainsKey(item.gameObject.name))
                {
                    string x = item.gameObject.name;
                    generatorDictionary.Add(item.gameObject.name, item);
                }
            }
        }
        foreach (var item in generatorDictionary)
        {
            Debug.Log(item);
        }
        p = FindAnyObjectByType<PrestigeButtonHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
        #if UNITY_EDITOR
        if (Input.GetKeyUp(KeyCode.D))
        {
            SaveSystemManager.Instance.DeleteSaveAndResetGame();
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            SaveSystemManager.Instance.Load();
        }
        #endif
        if (Input.GetKeyUp(KeyCode.A))
        {
            AchievementUI.ToggleUI();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            StatsUI.ToggleUI();
        }
        if (Input.GetKeyUp(KeyCode.P))
        {
            p.PrestigeButtonPressed();
            p.ConfirmPrestige();
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PauseMenuManager.Instance.ToggleSettingsUI();
        }
        UpgradeOnButton();

    }

    void HandleGeneratorUpgrade(string generatorKey)
    {
        AudioManager.Instance.AudioSystemSO.PlayUISound(SoundName.ButtonClick_1);

        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            Debug.Log("Control key held down during upgrade.");
            generatorDictionary[generatorKey]?.UpgradeGeneratorMax();
        }
        else if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            Debug.Log("Shift key held down during upgrade.");
            generatorDictionary[generatorKey]?.UpgradeGeneratorAmount(10);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Space key held down during upgrade.");
            generatorDictionary[generatorKey]?.UpgradeGeneratorAmount(25);
        }
        else
        {
            Debug.Log("No modifier key held. Default upgrade.");
            generatorDictionary[generatorKey]?.UpgradeGenerator();
        }
    }
    private void UpgradeOnButton()
    {
        for (int i = 1; i <= 9; i++)
        {
            if (Input.GetKeyUp(KeyCode.Alpha0 + i))
            {
                HandleGeneratorUpgrade($"Generator {i}");
                return;
            }
        }

        // Alpha0 = Generator 10
        if (Input.GetKeyUp(KeyCode.Alpha0))
        {
            HandleGeneratorUpgrade("Generator 10");
            return;
        }

        // F1–F5 = Generators 11–15
        for (int i = 1; i <= 5; i++)
        {
            if (Input.GetKeyUp(KeyCode.F1 + (i - 1)))
            {
                HandleGeneratorUpgrade($"Generator {10 + i}");
                return;
            }
        }
    }
}
