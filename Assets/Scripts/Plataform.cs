using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Plataform : MonoBehaviour
{
    private Animator _anim;
    private bool _hasPlayerBeen;
    private Coroutine _destroyRoutine;

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

        _destroyRoutine = StartCoroutine(DestroyPlatRoutine());

    }

    private void FixedUpdate()
    {
        if (CheckPlayer())
            DestroyPlataform();
    }

    private bool CheckPlayer()
    {
        if (HasPlayer)
            _hasPlayerBeen = true;

        return !HasPlayer && _hasPlayerBeen;
    }

    private void DestroyPlataform()
    {
        _destroyRoutine = StartCoroutine(DestroyPlatRoutine());
    }

    private IEnumerator DestroyPlatRoutine()
    {
        _anim.SetBool("Die", true);

        yield return FadeOut();

        StopCoroutine(_destroyRoutine);
        Destroy(gameObject);
    }

    private IEnumerator FadeOut()
    {
        Material[] mats;
        mats = gameObject.GetComponentInChildren<MeshRenderer>().materials;
        
        for(float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            foreach (Material m in mats)
            {
                Color c = m.color;
                c.a = f;

                m.color = c;
            }

            yield return new WaitForSeconds(0.05f);
        }
    }
}
