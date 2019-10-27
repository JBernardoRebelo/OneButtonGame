using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePoint : MonoBehaviour
{
    [SerializeField] private float _degreesPerSecond = 15.0f;
    [SerializeField] private float _amplitude = 0.5f;
    [SerializeField] private float _frequency = 1f;
    [SerializeField] private GameObject _deathParticles;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    private void Start()
    {
        posOffset = transform.position;
    }

    private void FixedUpdate()
    {
        // Spin object around Y-Axis
        transform.Rotate(new Vector3(0f, Time.fixedDeltaTime * _degreesPerSecond, 0f), Space.World);

        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * _frequency) * _amplitude;

        transform.position = tempPos;
    }

    public void KillHP()
    {
        if (_deathParticles)
            Instantiate(_deathParticles, transform);
        Destroy(gameObject);
    }
}
