using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Plataform : MonoBehaviour, ISpawnable, IMovable
{
    [SerializeField] private GameObject[] _enemiesPrefab;
    [SerializeField] private float _playerDetectionRange;

    private Animator _anim;
    private bool _hasPlayerBeen;
    private GameObject _spawnedEnemy;
    private bool _move;
    private float _time;
    private float _speed;

    public bool HasPlayer
    {
        get
        {
            Collider[] col = new Collider[2];
            Physics.OverlapSphereNonAlloc(transform.position, _playerDetectionRange,
                col, LayerMask.GetMask("Player"));

            return col[0];
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _playerDetectionRange);
    }

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _hasPlayerBeen = false;
        _move = false;
    }

    private void FixedUpdate()
    {
        if (CheckPlayer())
        {
            DestroyPlataform();
        }

        if (_move)
        {
            // Translate position to new position
            if (_time > 0.01f)
            {
                transform.Translate(-Vector3.forward *_speed * Time.deltaTime);
                _time -= Time.deltaTime;
            }
            else
                _move = false;
        }
    }

    private bool CheckPlayer()
    {
        if (HasPlayer)
            _hasPlayerBeen = true;

        return !HasPlayer && _hasPlayerBeen;
    }

    private void DestroyPlataform()
    {
        _anim.SetBool("Die", true);
    }

    public void DestroyObject()
    {
        Debug.Log("Dead");
        Destroy(gameObject);
    }

    public void Spawn()
    {
        _spawnedEnemy = Instantiate(_enemiesPrefab[Random.Range(0, _enemiesPrefab.Length - 1)]
            , Vector3.zero, transform.rotation);

        _spawnedEnemy.transform.SetParent(gameObject.transform);
    }

    public void Move(float speed = 1)
    {
        _speed = speed;
        _time = 2f / _speed;
        _move = true;
    }
}
