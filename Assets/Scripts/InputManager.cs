using UnityEngine;

public class InputManager : MonoBehaviour
{
    public AchievementContainerUIScript AchievementUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveSystemManager.Instance.Save();
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            SaveSystemManager.Instance.Load();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            SaveSystemManager.Instance.DeleteSaveAndResetGame();
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            AchievementUI.ToggleUI();
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PauseMenuManager.Instance.ToggleSettingsUI();
        }
    }
}
