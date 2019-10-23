using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    // Class vars
    [SerializeField] private GameObject _pltfrm = default;
    private Queue<GameObject> _world;
    private Vector3 _offset;
    private Quaternion _tileRot;


    // Start is called before the first frame update
    void Start()
    {
        _tileRot = Quaternion.identity;

        _world = new Queue<GameObject>(10);

        GameObject plataform = default;

        for (int i = 0; i < 10; i++)
        {
            _offset = new Vector3(0f, 0f, (2* i));

            plataform = Instantiate(_pltfrm, _offset, _tileRot);

            plataform.transform.SetParent(transform);

            _world.Enqueue(plataform);
        }
    }

    void FixedUpdate()
    {
        EnqueueNewTile(_world);
    }

    private void EnqueueNewTile(Queue<GameObject> world)
    {
        if (world.Peek() == null)
        {
            world.Dequeue();
        }

        if (world.Count < 10)
        {
            _offset = new Vector3(0f, 0f, 18f);

            GameObject plataform = Instantiate(_pltfrm, _offset, _tileRot);

            world.Enqueue(plataform);


        }

        else if (world.Count == 10)
        {

        }
    }
}
