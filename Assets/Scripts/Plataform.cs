using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Plataform : MonoBehaviour, ISpawnable, IMovable
{
    [SerializeField] private GameObject[] _enemiesPrefab;
    [SerializeField] [Range(0, 1f)] private float _speed;

    private Animator _anim;
    private bool _hasPlayerBeen;
    private GameObject _spawnedEnemy;

    public bool HasPlayer
    {
        get
        {
            Collider[] col = new Collider[2];
            Physics.OverlapSphereNonAlloc(transform.position, 2, col, LayerMask.GetMask("Player"));

            return col[0];
        }
    }

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _hasPlayerBeen = false;
    }

    private void FixedUpdate()
    {
        if (CheckPlayer())
        {
            DestroyPlataform();
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
        Destroy(gameObject);
    }

    public void Spawn()
    {
        _spawnedEnemy = Instantiate(_enemiesPrefab[Random.Range(0, _enemiesPrefab.Length - 1)]
            , Vector3.zero, transform.rotation);

        _spawnedEnemy.transform.SetParent(gameObject.transform);
    }

    public void Move(int moveAmount = 1)
    {
        Vector3 newPosition;

        newPosition = transform.position;
        newPosition.z = -2 * moveAmount;

        transform.position = Vector3.Lerp(transform.position, newPosition, _speed);
    }

}
