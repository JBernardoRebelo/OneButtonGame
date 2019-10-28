using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private LayerMask _colLayer;
    [SerializeField] private Color[]   _matColors;
    public GameObject _deathParticles;
    [SerializeField] private float _playerDetectionRange;
    private PlayerState _killState;
    private Material    _enemyMat;
    private Collision   _hit;

    [Header("Sound")]
    public AudioClip enemyDeath;

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
            Player p = onPlayer.GetComponentInParent<Player>();

            if (p)
            {
                if (p.State != _killState)
                    p.Damage();
            }

            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        // Sound
        SoundManager.PlaySound(enemyDeath, 10f, Random.Range(1.0f, 1.5f));
        var o = Instantiate(_deathParticles, transform.position, transform.rotation);
        ParticleSystem p = o.GetComponent<ParticleSystem>();
        p.startColor = _enemyMat.color;
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _playerDetectionRange);
    }
}
