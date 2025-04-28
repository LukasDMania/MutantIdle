using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsScreenContainerUI : MonoBehaviour
{
    public GameObject MainContainer;

    public ScrollRect ScrollRect;

    [Header("Actual Variables")]
    public DoubleVariable TotalTicks;
    public DoubleVariable AchievementsUnlocked;
    public DoubleVariable GeneratorLevelTotal;
    public DoubleVariable PowerupsClickedAmount;
    public DoubleVariable TotalPrestiges;
    public FloatVariable TotalPlayTimeInSeconds;

    [Header("TextMeshPros For The Values")]
    public TextMeshProUGUI TotalTicksT;
    public TextMeshProUGUI AchievementsUnlockedT;
    public TextMeshProUGUI GeneratorLevelTotalT;
    public TextMeshProUGUI PowerupsClickedAmountT;
    public TextMeshProUGUI TotalPrestigesT;
    public TextMeshProUGUI TotalPlayTimeInSecondsT;

    [HideInInspector]
    public bool UIIsOpen;
    void Start()
    { 
        UIIsOpen = true;
        CloseUI();
        ScrollRect.verticalNormalizedPosition = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (UIIsOpen)
        {
            UpdateValues();
        }
    }

    private void UpdateValues()
    {
        TotalTicksT.text = TotalTicks.Value.ToString();
        AchievementsUnlockedT.text = AchievementsUnlocked.Value.ToString();
        GeneratorLevelTotalT.text = GeneratorLevelTotal.Value.ToString();
        PowerupsClickedAmountT.text = PowerupsClickedAmount.Value.ToString();
        TotalPrestigesT.text = TotalPrestiges.Value.ToString();
        TotalPlayTimeInSecondsT.text = FormatTime(TotalPlayTimeInSeconds.Value);
    }

    public void OpenUI()
    {
        if (!UIIsOpen)
        {
            MainContainer.SetActive(true);
            UIIsOpen = true;
        }
    }
    public void CloseUI()
    {
        if (UIIsOpen)
        {
            MainContainer.SetActive(false);
            UIIsOpen = false;
        }
    }
    public void ToggleUI()
    {

        if (UIIsOpen)
            CloseUI();
        else
            OpenUI();
    }

    private string FormatTime(float totalSeconds)
    {
        int seconds = (int)totalSeconds;

        int days = seconds / 86400;
        seconds %= 86400;

        int hours = seconds / 3600;
        seconds %= 3600;

        int minutes = seconds / 60;
        seconds %= 60;

        return $"{days}d {hours}h {minutes}m {seconds}s";
    }

}
