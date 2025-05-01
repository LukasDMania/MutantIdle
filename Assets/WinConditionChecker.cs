using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class WinConditionChecker : MonoBehaviour
{
    private Generator FinalGenerator;
    public Transform WinScreen;
    public ParticleSystem WinPs;

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

    private bool hasWon;
    private void Start()
    {
        GameObject gameObject = GameObject.Find("Generator 15");
        FinalGenerator = gameObject.GetComponent<Generator>();
        if (FinalGenerator.GeneratorLevel >= 100)
        {
            hasWon = true;
        }
        else
        {
            hasWon = false;
        }
        WinScreen.gameObject.SetActive(false);
        Debug.Log("HasWOnVar On Start" + hasWon);
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
        Debug.Log("HasWon func = " + HasWon());
        if (!HasWon())
        {
            return;
        }

        Debug.Log("HasWonVar" + hasWon);
        if (!hasWon)
        {
            WinPs.Play();
            StartCoroutine(WaitForParticleToFinish(WinPs));
            hasWon=true;
        }

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
        if (FinalGenerator.GeneratorLevel >= 100)
        {
            return true;
        }
        return false;
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

    IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, t / duration);
            yield return null;
        }
        cg.alpha = end;

        string filename = $"Screenshot_WinScreen.png";
        ScreenCapture.CaptureScreenshot(filename);
    }
    IEnumerator WaitForParticleToFinish(ParticleSystem system)
    {
        yield return new WaitForSeconds(6);
        Debug.Log("Particle system finished!");

        WinScreen.gameObject.SetActive(true);
        PopulateData();
        CanvasGroup group = WinScreen.GetComponent<CanvasGroup>();
        StartCoroutine(FadeCanvasGroup(group, 0f, 1f, 6f));
        
    }
}
