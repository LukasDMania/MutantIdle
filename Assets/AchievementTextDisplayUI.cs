using DG.Tweening;
using TMPro;
using UnityEngine;

public class AchievementTextDisplayUI : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;


    private void Awake()
    {
        if (textMeshProUGUI == null)
        {
            Debug.LogError("TextMeshProUGUI component is not assigned!  Disabling script.");
            enabled = false;
            return;
        }
        textMeshProUGUI.alpha = 0f;
        textMeshProUGUI.gameObject.SetActive(false);
    }
    public void DisplayAchievementText(string text) 
    {
        textMeshProUGUI.DOFade(0, 0).Kill();
        textMeshProUGUI.gameObject.SetActive(true);
        textMeshProUGUI.text = text;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(textMeshProUGUI.DOFade(1f, 1.5f));
        sequence.AppendInterval(2f);                               
        sequence.Append(textMeshProUGUI.DOFade(0f, 2f));
        sequence.OnComplete(() =>
        {
            textMeshProUGUI.gameObject.SetActive(false);
        });
    }
}
