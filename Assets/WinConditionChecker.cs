using System;
using TMPro;
using UnityEngine;

public class WinConditionChecker : MonoBehaviour
{
    private Generator FinalGenerator;
    public Transform WinScreen;

    [Header("Actual Variables")]
    public DoubleVariable TotalTicks;
    public DoubleVariable AchievementsUnlocked;
    public DoubleVariable PowerupsClickedAmount;
    public DoubleVariable TotalPrestiges;
    public FloatVariable TotalPlayTimeInSeconds;
    public DoubleVariable TotalPrestigePoints;

    public TextMeshProUGUI TotalTicksText;
    public TextMeshProUGUI AchievementsUnlockedText;
    public TextMeshProUGUI PowerupsClickedAmountText;
    public TextMeshProUGUI TotalPrestigesText;
    public TextMeshProUGUI TotalPlayTimeInSecondsText;
    public TextMeshProUGUI TotalPrestigePointsText;

    public NumberFormatterSO NumberFormatterSO;

    private void Start()
    {
        GameObject gameObject = GameObject.Find("Generator 15");
        FinalGenerator = gameObject.GetComponent<Generator>();
        WinScreen.gameObject.SetActive(false);
    }

    public void CloseScreen() 
    {
        WinScreen.gameObject.SetActive(false);
    }
    public void SaveAndQuit() 
    {
        SaveSystemManager.Instance.QuitGame();
    }
    public void ResetAll() 
    {
        SaveSystemManager.Instance.DeleteSaveAndResetGame();
        CloseScreen();
    }
    public void ShowWinScreenOnWin()
    {
        if (!HasWon())
        {
            return;
        }
        WinScreen.gameObject.SetActive(true);
        PopulateData();
        string filename = $"Screenshot_WinScreen.png";
        ScreenCapture.CaptureScreenshot(filename);
    }

    private void PopulateData()
    {
        TotalTicksText.text = TotalTicks.Value.ToString();
        AchievementsUnlockedText.text = AchievementsUnlocked.Value.ToString();
        PowerupsClickedAmountText.text = PowerupsClickedAmount.Value.ToString();
        TotalPrestigesText.text = TotalPrestiges.Value.ToString();
        TotalPlayTimeInSecondsText.text = FormatTime(TotalPlayTimeInSeconds.Value);
        TotalPrestigePointsText.text = NumberFormatterSO.FormatNumber(TotalPrestigePoints.Value, NumberFormatterSO.FormatType.Suffix);
    }


    public bool HasWon() 
    {
        return FinalGenerator.GeneratorLevel >= 100;
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

        return $"{days}d {hours}h {minutes}m";
    }
}
