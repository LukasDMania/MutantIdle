using System;
using UnityEngine;

/// <summary>
/// ScriptableObject for formatting large numbers in idle games
/// with options for scientific notation or suffix notation (K, M, B, T, etc.)
/// </summary>
[CreateAssetMenu(fileName = "NumberFormatter", menuName = "Number FormatterSO")]
public class NumberFormatterSO : ScriptableObject
{
    // Format type enum
    public enum FormatType
    {
        Scientific,
        Suffix
    }

    [Tooltip("Default formatting type to use")]
    public FormatType defaultFormatType = FormatType.Suffix;

    [Tooltip("Maximum number of decimal places to show (0-5)")]
    [Range(0, 5)]
    public int maxDecimalPlaces = 2;

    [Tooltip("Custom suffixes for large numbers (leave empty to use defaults)")]
    public string[] customSuffixes;

    // Default suffixes if custom ones aren't provided
    private static readonly string[] DefaultSuffixes = new string[]
    {
        "", "K", "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "Oc", "No",
        "Dc", "UDc", "DDc", "TDc", "QaDc", "QiDc", "SxDc", "SpDc", "ODc", "NDc",
        "Vi", "UVi", "DVi", "TVi", "QaVi", "QiVi", "SxVi", "SpVi", "OVi", "NVi",
        "Tg", "UTg", "DTg", "TTg", "QaTg", "QiTg", "SxTg", "SpTg", "OTg", "NTg"
    };

    private void OnEnable()
    {
        // If no custom suffixes are provided, initialize with defaults
        if (customSuffixes == null || customSuffixes.Length == 0)
        {
            customSuffixes = DefaultSuffixes;
        }
    }

    /// <summary>
    /// Format a number using the default format type
    /// </summary>
    /// <param name="number">The number to format</param>
    /// <returns>Formatted string representation of the number</returns>
    public string FormatNumber(double number)
    {
        return FormatNumber(number, defaultFormatType);
    }

    /// <summary>
    /// Format a number according to the specified format type
    /// </summary>
    /// <param name="number">The number to format</param>
    /// <param name="formatType">The format type to use (Scientific or Suffix)</param>
    /// <returns>Formatted string representation of the number</returns>
    public string FormatNumber(double number, FormatType formatType)
    {
        // Handle zero and negative numbers
        if (number == 0)
            return "0";

        if (number < 0)
            return "-" + FormatNumber(-number, formatType);

        // Handle NaN and infinity
        if (double.IsNaN(number))
            return "NaN";

        if (double.IsInfinity(number))
            return "Infinity";

        // Choose the appropriate formatting method
        switch (formatType)
        {
            case FormatType.Scientific:
                return FormatScientific(number);
            case FormatType.Suffix:
                return FormatSuffix(number);
            default:
                return number.ToString();
        }
    }

    /// <summary>
    /// Format a number using scientific notation
    /// </summary>
    private string FormatScientific(double number)
    {
        if (number < 1000)
            return FormatWithDecimals(number);

        // Get the exponent (power of 10)
        int exponent = (int)Math.Floor(Math.Log10(number));

        // Get the mantissa (the number part)
        double mantissa = number / Math.Pow(10, exponent);

        return FormatWithDecimals(mantissa) + "e" + exponent;
    }

    /// <summary>
    /// Format a number using suffix notation (K, M, B, T, etc.)
    /// </summary>
    private string FormatSuffix(double number)
    {
        if (number < 1000)
            return FormatWithDecimals(number);

        // Calculate the suffix index based on the magnitude
        int suffixIndex = (int)Math.Floor(Math.Log10(number) / 3);

        // Ensure we don't exceed our suffix array
        if (suffixIndex >= customSuffixes.Length)
        {
            // Fall back to scientific notation for extremely large numbers
            return FormatScientific(number);
        }

        // Scale the number to the appropriate range (between 1 and 999.99)
        double scaledNumber = number / Math.Pow(10, suffixIndex * 3);

        return FormatWithDecimals(scaledNumber) + customSuffixes[suffixIndex];
    }

    /// <summary>
    /// Helper method to format a number with the configured decimal places
    /// Removes trailing zeros and decimal point if not needed
    /// </summary>
    private string FormatWithDecimals(double number)
    {
        // Format with the configured decimal places
        string formatted = number.ToString("F" + maxDecimalPlaces);


        // Remove decimal point if it's the last character
        if (formatted.EndsWith("."))
            formatted = formatted.TrimEnd('.');

        return formatted;
    }

    /// <summary>
    /// Try to parse a formatted number back to its original value
    /// </summary>
    /// <param name="formattedNumber">The formatted number string</param>
    /// <param name="result">The parsed double value</param>
    /// <returns>True if parsing was successful, false otherwise</returns>
    public bool TryParse(string formattedNumber, out double result)
    {
        result = 0;

        if (string.IsNullOrEmpty(formattedNumber))
            return false;

        // Handle scientific notation
        if (formattedNumber.Contains("e"))
        {
            string[] parts = formattedNumber.Split('e');
            if (parts.Length != 2)
                return false;

            if (!double.TryParse(parts[0], out double mantissa) ||
                !int.TryParse(parts[1], out int exponent))
                return false;

            result = mantissa * Math.Pow(10, exponent);
            return true;
        }

        // Handle suffix notation
        for (int i = customSuffixes.Length - 1; i > 0; i--)
        {
            if (formattedNumber.EndsWith(customSuffixes[i]))
            {
                string numberPart = formattedNumber.Substring(0, formattedNumber.Length - customSuffixes[i].Length);
                if (double.TryParse(numberPart, out double value))
                {
                    result = value * Math.Pow(10, i * 3);
                    return true;
                }
                return false;
            }
        }

        // Handle plain numbers
        return double.TryParse(formattedNumber, out result);
    }
}