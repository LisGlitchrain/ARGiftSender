using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InstantFillText : MonoBehaviour
{
    [TextArea]
    public string text;
    public TextMeshProUGUI textToFill;

    public void FillText() => textToFill.text = text;
    public void DeleteText() => textToFill.text = string.Empty;
}
