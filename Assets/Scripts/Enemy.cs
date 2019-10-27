using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private LayerMask _colLayer;
    [SerializeField] private Color[]   _matColors;
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private float _playerDetectionRange;
    private PlayerState _killState;
    private Material    _enemyMat;
    private Collision   _hit;

    public Collider onPlayer
    {
        get
        {
            Collider[] col = new Collider[2];
            Physics.OverlapSphereNonAlloc(transform.position, _playerDetectionRange,
                col, _colLayer);

            return col[0];
        }
    }

    private void Start()
    {
        int stateIndex = Random.Range(0, 3);
        _enemyMat = GetComponentInChildren<MeshRenderer>().material;

        _killState = (PlayerState)stateIndex;
        _enemyMat.color = _matColors[stateIndex];
    }

    private void FixedUpdate()
    {
        CheckForCol();
    }

    private void CheckForCol()
    {
        if (onPlayer)
        {
            Player p = onPlayer.GetComponent<Player>();

            //if (p.State != _killState)
            //    p.Damage();
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        if (deathParticles)
            Instantiate(deathParticles, transform);

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _playerDetectionRange);
    }
}
