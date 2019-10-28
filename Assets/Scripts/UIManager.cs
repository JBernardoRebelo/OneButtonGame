using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI _cyclesText = null;
    [SerializeField]private Player _player;

    private const string CYCLES_STRING = "CYCLES: ";

    private void Start()
    {
        _cyclesText.text = CYCLES_STRING + 0.ToString();
    }

    private void Update()
    {
        _cyclesText.text = CYCLES_STRING + _player.StateCycles.ToString();
    }
}
