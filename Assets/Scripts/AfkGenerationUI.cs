using TMPro;
using UnityEngine;

public class AfkGenerationUI : MonoBehaviour
{
    public GameObject Container;
    public DoubleVariable AfkCurrencyGenerated;
    public NumberFormatterSO NumberFormatterSO;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool hidden = true;
    void Start()
    {
        Debug.Log("LAST PLAYED KEY EXISTS AFKUI: " + PlayerPrefs.HasKey("LastPlayed"));
        if (PlayerPrefs.HasKey("LastPlayed"))
        {
            ShowAfkGainContainer();
        }
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && !hidden)
        {
            HideAfkGainContainer();
            hidden = true;
        }
    }

    public void ShowAfkGainContainer() 
    {
        Container.SetActive(true);
        hidden = false;
        TextMeshProUGUI textMeshProUGUI = Container.GetComponentInChildren<TextMeshProUGUI>();

        textMeshProUGUI.text = $"You've earned {NumberFormatterSO.FormatNumber(AfkCurrencyGenerated.Value, NumberFormatterSO.FormatType.Suffix)} DNA While you were away";
    }

    public void HideAfkGainContainer() 
    {
        Container.SetActive(false);
    }
}
