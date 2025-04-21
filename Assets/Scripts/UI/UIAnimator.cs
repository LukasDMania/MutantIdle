using DG.Tweening;
using UnityEngine;

public class UIAnimator : MonoBehaviour
{
    public RectTransform panel; // Reference to your panel's RectTransform
    public Vector2 hiddenPosition; // Position where the panel is hidden
    public Vector2 visiblePosition; // Position where the panel is visible
    public float duration = 0.5f; // Animation duration

    private bool isPanelVisible = true;

    // Function to move the panel in and out
    private void Start()
    {
    }
    public void TogglePanel()
    {
        if (isPanelVisible)
        {
            // Move the panel to the hidden position
            panel.DOAnchorPos(hiddenPosition, duration);
        }
        else
        {
            // Move the panel to the visible position
            panel.DOAnchorPos(visiblePosition, duration);
        }

        isPanelVisible = !isPanelVisible; // Toggle the panel visibility state
    }
}
