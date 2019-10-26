using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    // Colors for each player state
    [SerializeField] private Color[] _matColors;
    private PlayerState _currentChoice;
    private Color _currentColor;
    // Reflects the current selected Player State
    private Material _playerMat;
    // Size of PlayerState enum
    private int _numberOfStates;
    private int _choice;
    public UnityEvent _choiceText;

    public PlayerState State => _currentChoice;
    public Color CurrentColor => _currentColor;
    public int StateCycles { get; private set; }

    private void Start()
    {
        _playerMat = GetComponentInChildren<MeshRenderer>().material;
        _numberOfStates = 3;
        _choice = 0;
        _currentChoice = PlayerState.Default;
        StateCycles = 0;
    }

    private void Update()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _choice += 1;
            _currentChoice = (PlayerState)(_choice % _numberOfStates);

            UpdateMaterial();
            _choiceText.Invoke();

            if (_currentChoice == PlayerState.Default)
                StateCycles++;
        }
    }

    private void UpdateMaterial()
    {
        _playerMat.color = _matColors[(int)_currentChoice];
        _currentColor = _playerMat.color;
    }
}
