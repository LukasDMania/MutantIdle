using UnityEngine;

public class InputManager : MonoBehaviour
{
    public AchievementContainerUIScript AchievementUI;
    public StatsScreenContainerUI StatsUI;

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
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PauseMenuManager.Instance.ToggleSettingsUI();
        }
    }
}
