using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public TextMeshPro _stateText;

    public void SetText(string text, Color stateColor)
    {
        _stateText.color = Color.red;
        _stateText.text  = text.ToUpper();
    }
}
