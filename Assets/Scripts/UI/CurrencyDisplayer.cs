using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyDisplayer : MonoBehaviour 
{
    [SerializeField]
    private DoubleVariable _currentCurrency;
    [SerializeField]
    private TextMeshPro _textToDisplayCurrency;
    [SerializeField]
    private DoubleVariable _tickInterval;

    private double _targetCurrency = 0;
    private double _previousCurrencyAmount = 0;
    private float _timeElapsed = 0f;
    private float _displayCurrencyFloat;

    private void Update()
    {
        ClampCurrencyChange();
    }

    private void ClampCurrencyChange() 
    {
        if (_currentCurrency.Value != _targetCurrency)
        {
            _previousCurrencyAmount = _targetCurrency;
            _targetCurrency = _currentCurrency.Value;
            _timeElapsed = 0f;
        }

        if (_targetCurrency > _previousCurrencyAmount)
        {
            _timeElapsed += Time.deltaTime;
            _displayCurrencyFloat = Mathf.Lerp((float)_previousCurrencyAmount, (float)_targetCurrency, _timeElapsed / (float)_tickInterval.Value);
        }
        else
        {
            _displayCurrencyFloat = (float)_targetCurrency;
        }

        int currencyToDisplay = Mathf.FloorToInt(_displayCurrencyFloat);
        _textToDisplayCurrency.text = currencyToDisplay.ToString();
    }
}
