using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Particles : MonoBehaviour
{
    [SerializeField] private float _timeToDestroy = 1.5f;
    private ParticleSystem _pSystem;

    private void Start()
    {
        _pSystem = GetComponent<ParticleSystem>();
        _pSystem.Emit(100);
        Destroy(gameObject, _timeToDestroy);
    }

    public void SetColor(Color color)
    {
        _pSystem.startColor = color;
    }
}
