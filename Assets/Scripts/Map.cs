using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    // Class vars
    [SerializeField] private Plataform _pltfrm = default;
    private Queue<Plataform> _world;
    private Vector3 _offset;
    private Quaternion _tileRot;


    // Start is called before the first frame update
    void Start()
    {
        _tileRot = Quaternion.identity;

        _world = new Queue<Plataform>(5);

        Plataform plataform = Instantiate(_pltfrm, _offset, _tileRot);
        _world.Enqueue(plataform);

        for (int i = 0; i < 4; i++)
        {
            _offset = new Vector3(0f, 0f, (i + 2));

            plataform = Instantiate(_pltfrm, _offset, _tileRot);

            _world.Enqueue(plataform);
        }
    }

    void FixedUpdate()
    {
        EnqueueNewTile(_world);
    }

    private void EnqueueNewTile(Queue<Plataform> world)
    {
        if (world.Peek() == null)
        {
            world.Dequeue();
        }

        if (world.Count < 5)
        {
            _offset = new Vector3(0f, 0f, 10f);
            
            Plataform plataform = Instantiate(_pltfrm, _offset, _tileRot);

            world.Enqueue(plataform);


        }

        else if (world.Count == 5)
        {

        }
    }
}
