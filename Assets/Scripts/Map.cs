using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    // Class vars
    [SerializeField] private Plataform _pltfrm = default;
    [SerializeField] private Player _player = null;
    [Tooltip("This value determines how many times the player has to pass " +
        "through the default state to increase the map speed")]
    [SerializeField] private int _cyclesToIncrease;

    private Queue<Plataform> _world;
    private Vector3 _offset;
    private Quaternion _tileRot;


    // Start is called before the first frame update
    void Start()
    {
        _tileRot = Quaternion.identity;

        _world = new Queue<Plataform>(10);

        Plataform plataform = default;

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

    private void EnqueueNewTile(Queue<Plataform> world)
    {
        if (world.Peek() == null)
        {
            world.Dequeue();
        }

        if (world.Count < 10)
        {
            _offset = new Vector3(0f, 0f, 18f);

            Plataform plataform = Instantiate(_pltfrm, _offset, _tileRot);

            world.Enqueue(plataform);


        }

        else if (world.Count == 10)
        {
            foreach (Plataform p in world)
            {
                p.Move(PlataformMoveSpeed());
            }
        }
    }

    private float PlataformMoveSpeed()
    {
        float speed = 1;

        if (_player.StateCycles > _cyclesToIncrease)
            speed = _player.StateCycles / _cyclesToIncrease;

        return speed;
    }
}
