using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTextUpdater : MonoBehaviour
{
    [SerializeField] private Player    _player;
    [SerializeField] private GameObject _textPrefab;
    [SerializeField] private Transform _holder;
    [SerializeField] private float lifeTime = 5f;
    private StateText _txt;

    public void UpdateState()
    {
        GameObject txtObj = Instantiate(_textPrefab, _holder);
        _txt = txtObj.GetComponent<StateText>();
        _txt.SetStateText(_player.State, _player.CurrentColor);
        Destroy(txtObj, lifeTime);
    }
}
