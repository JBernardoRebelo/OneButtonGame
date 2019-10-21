using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    // Class vars
    [SerializeField] private Plataform blockLarge;
    private Queue<Plataform> world;

    // Start is called before the first frame update
    void Start()
    {
        world = new Queue<Plataform>(5);

        for (int i = 0; i < world.Count; i++)
        {
            world.Enqueue(Instantiate(blockLarge));
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
