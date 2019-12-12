using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class AsteroidManager : MonoBehaviour
{
    EntityManager manager;
    [SerializeField]
    public GameObject asteroidPrefab;
    // Start is called before the first frame update
    void Start()
    {
        manager = World.Active.EntityManager;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnAsteroidECS(Vector3 rotation)
    {
        Entity asteroid = manager.Instantiate(asteroidPrefab);
        //manager.SetComponentData(asteroid, new Position)
    }
}
