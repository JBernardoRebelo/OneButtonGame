using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeBar : MonoBehaviour
{
    [SerializeField] private LifePoint _lifePointPrefab;
    [SerializeField] private Player _player;

    private List<LifePoint> _lifePoints;
    private void Start()
    {
        _lifePoints = new List<LifePoint>(_player.CurrentHP);
        //Instantiate(_lifePointPrefab, spawnPosition, transform.rotation);

        for (int i = 0; i < _player.MAXHP; i++)
        {
            Vector3 spawnPosition = transform.position;
            spawnPosition.z -= i;
            spawnPosition.y += i/2.5f;

            LifePoint newPoint = Instantiate(_lifePointPrefab, spawnPosition, transform.rotation);
            newPoint.transform.SetParent(transform);
            _lifePoints.Add(newPoint);
        }
    }

    private void Update()
    {
        UpdateLifeBar();
    }

    private void UpdateLifeBar()
    {
        if (_lifePoints.Count > _player.CurrentHP)
        {
            _lifePoints[_player.CurrentHP].KillHP();
            _lifePoints.RemoveAt(_player.CurrentHP);
        }
    }
}
