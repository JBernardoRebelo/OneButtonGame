using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    // Colors for each player state
    [SerializeField] private Color[] _matColors;
    [SerializeField] private int _playerMAXHP;
    [SerializeField] private GameObject _deathParticles;
    [SerializeField] private float _invulnerabilityDuration = 1.0f;

    // Sound
    [Header("Sound")]
    public AudioClip getHit;
    public AudioClip shapeShift;

    private PlayerState _currentChoice;
    private Color _currentColor;
    // Reflects the current selected Player State
    private Material _playerMat;
    // Size of PlayerState enum
    private int _numberOfStates;
    private int _choice;
    private int _playerHP;
    private float _invulnerabilityTimer;

    public UnityEvent _choiceText;

    private bool Invulnerable
    {
        get
        {
            if (_invulnerabilityTimer > 0.0f) return true;

            return false;
        }
        set
        {
            if (value)
            {
                _invulnerabilityTimer = _invulnerabilityDuration;
            }
            else
            {
                _invulnerabilityTimer = 0.0f;
            }
        }
    }

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
        UpdateInvuln();
    }

    protected virtual void SetInvulnerabilitFX(bool b)
    {
        if (b)
            _playerMat.color = Color.gray;
        else
            _playerMat.color = _currentColor;
    }

    private void UpdateInvuln()
    {
        if (_invulnerabilityTimer > 0.0f)
        {
            _invulnerabilityTimer -= Time.deltaTime;

            SetInvulnerabilitFX((Mathf.FloorToInt(_invulnerabilityTimer * 10.0f) % 2) == 0);

            if (_invulnerabilityTimer <= 0.0f)
            {
                SetInvulnerabilitFX(false);
            }
        }
    }

    private void CheckPlayerHP()
    {
        if (_playerHP <= 0)
        {
            if (_deathParticles)
                Instantiate(_deathParticles, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {  
            // Sound
            SoundManager.PlaySound(shapeShift, Random.Range(1.0f, 1.1f));

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
        // Sound
        SoundManager.PlaySound(getHit, Random.Range(10f, 11f), Random.Range(1.0f, 1.5f));
        _playerHP--;
        Invulnerable = true;
    }

    private void UpdateMaterial()
    {
        _playerMat.color = _matColors[(int)_currentChoice];
        _currentColor = _playerMat.color;
    }
}
