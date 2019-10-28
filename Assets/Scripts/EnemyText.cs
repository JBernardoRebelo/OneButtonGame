using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyText : MonoBehaviour
{
    [SerializeField] private string[] _enemyText;
    [SerializeField] private Color _textColor;
    private FloatingText _text;

    private void Awake()
    {
        string text;
        text = _enemyText[Random.Range(0, _enemyText.Length)];

        _text = GetComponentInChildren<FloatingText>();
        _text.SetText(text, _textColor);
    }
}
