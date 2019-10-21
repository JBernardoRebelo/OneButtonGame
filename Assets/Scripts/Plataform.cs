using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Plataform : MonoBehaviour
{
    private Animator _anim;
    private bool _hasPlayerBeen;

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

        _anim.SetBool("Die", true);
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
}
