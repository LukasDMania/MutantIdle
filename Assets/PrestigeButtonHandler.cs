using UnityEngine;

public class PrestigeButtonHandler : MonoBehaviour
{
    public GameObject PrestigeButton;
    public GameObject PrestigeConfirmationPanel;

    public void ConfirmPrestige() 
    {
        PrestigeConfirmationPanel.SetActive(true);
    }
    public void CloseConfirmationPanel() 
    {
        PrestigeConfirmationPanel.SetActive(false);
    }
}
