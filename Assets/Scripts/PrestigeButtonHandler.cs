using UnityEngine;

public class PrestigeButtonHandler : MonoBehaviour
{
    public GameObject PrestigeButton;
    public GameObject PrestigeConfirmationPanel;

    public AchievementTextDisplayUI AchievementTextDisplayUI;

    public void ConfirmPrestige()
    {
        GameObject generator6 = GameObject.Find("Generator 6");
        if (generator6.GetComponent<Generator>().GeneratorLevel == 0)
        {
            AchievementTextDisplayUI.DisplayAchievementText("You need to have reached the 6th body part to prestige");
            return;
        }
        PrestigeConfirmationPanel.SetActive(true);
    }
    public void PrestigeButtonPressed() 
    {
        AudioManager.Instance.AudioSystemSO.PlayUISound(SoundName.ButtonClick_1);
    }
    public void CloseConfirmationPanel() 
    {
        PrestigeConfirmationPanel.SetActive(false);
    }
}
