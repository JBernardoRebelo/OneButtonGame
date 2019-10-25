using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StateText : MonoBehaviour
{
    public TextMeshProUGUI _stateText;

    public void SetStateText(PlayerState state, Color stateColor)
    {
        _stateText.color = stateColor;
        _stateText.text = state.ToString().ToUpper();
    }
}
