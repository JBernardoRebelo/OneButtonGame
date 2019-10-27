using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    // Colors for each player state
    [SerializeField] private Color[]    _matColors;
    [SerializeField] private int        _playerMAXHP;
    [SerializeField] private GameObject _deathParticles;

    private PlayerState _currentChoice;
    private Color       _currentColor;
    // Reflects the current selected Player State
    private Material    _playerMat;
    // Size of PlayerState enum
    private int         _numberOfStates;
    private int         _choice;
    private int         _playerHP;
    public UnityEvent   _choiceText;

    public PlayerState State => _currentChoice;
    public Color CurrentColor => _currentColor;
    public int StateCycles { get; private set; }
    public int CurrentHP => _playerHP;
    public int MAXHP => _playerMAXHP;

    private void Start()
    {
        _playerMat = GetComponentInChildren<MeshRenderer>().material;
        _numberOfStates = 3;
        _choice = 0;
        _currentChoice = PlayerState.Default;
        StateCycles = 0;

        _playerHP = _playerMAXHP;
    }

    private void Update()
    {
        UpdateState();
        CheckPlayerHP();
    }

    private void CheckPlayerHP()
    {
        if (_playerHP <= 0)
        {
            if (_deathParticles)
                Instantiate(_deathParticles, transform);
            Destroy(gameObject);
        }
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

    public void Damage()
    {
        _playerHP--;
    }

    private void UpdateMaterial()
    {
        _playerMat.color = _matColors[(int)_currentChoice];
        _currentColor = _playerMat.color;
    }
}
