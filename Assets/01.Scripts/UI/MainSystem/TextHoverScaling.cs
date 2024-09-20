using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using System.Text.RegularExpressions;

public class TextHoverScaling : MonoBehaviour
{
    private string pattern = @"(.*?)";

    public void SetWiggleText(TextMeshProUGUI targetText)
    {
        targetText.SetText($"<wiggle>{Regex.Replace(targetText.text, pattern, string.Empty)}</>");
    }

    public void SetDangleText(TextMeshProUGUI targetText)
    {
        targetText.SetText($"<dangle>{Regex.Replace(targetText.text, pattern, string.Empty)}</>");
    }
}
