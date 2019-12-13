using System.Collections.Generic;
using System.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using UnityEngine;


public class AsteroidManager : MonoBehaviour
{
    EntityManager manager;
    [SerializeField]
    public GameObject AsteroidPrefab;
    private Entity asteroidPrefab;
    Random random;
    List<Entity> entities;
    [SerializeField]
    public BulletManager bulletManager;
    [SerializeField]
    public GameObject ship;
    NativeArray<Entity> asteroids;

    // Start is called before the first frame update
    void Awake()
    {
        manager = World.Active.EntityManager;
        random = new Random();
        asteroidPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(AsteroidPrefab, World.Active);
        entities = new List<Entity>();

        SpawnAsteroidECS();
        SpawnAsteroidECS();
        SpawnAsteroidECS();
    }

    private void OnApplicationQuit()
    {
        for (int i = 0; i < entities.Count; i++)
        {
            entities.RemoveAt(i);
            i--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ManageCollisions();
    }

    void SpawnAsteroidECS()
    {
        entities.Add(manager.Instantiate(asteroidPrefab));

        manager.SetComponentData(entities[entities.Count - 1], new AsteroidData
        {
            position = new Vector3(Random.value * 19.9f - 10.0f, Random.value * 10.0f - 5.0f, 0),
            velocity = new Vector3(Random.value * 19.9f - 10.0f, Random.value * 10.0f - 5.0f, 0)
        });
        manager.SetComponentData(entities[entities.Count - 1], new Translation { Value = new Vector3(Random.value * 19.9f - 10.0f, Random.value * 10.0f - 5.0f, 0) });
        manager.SetComponentData(entities[entities.Count - 1], new Rotation { Value = Quaternion.Euler(90, 0, 0) });
    }

    void ManageCollisions()
    {
        CollideWithBullet();
        CollideWithShip();
    }

    void CollideWithShip()
    {
        for (int i = 0; i < entities.Count; i++)
        {
            if ((ship.transform.position - new Vector3(manager.GetComponentData<Translation>(entities[i]).Value.x, manager.GetComponentData<Translation>(entities[i]).Value.y, 0)).magnitude < 4)
            {
                if (ship != null)
                {
                    Destroy(ship);
                }
            }
        }
    }

    void CollideWithBullet()
    {
        List<Entity> list = ship.GetComponent<ShipController>().GetEntities();
       for (int i = 0; i < list.Count; i++)
        {
            for (int j = 0; j < entities.Count; j++)
            {
                if ((new Vector3(manager.GetComponentData<Translation>(list[i]).Value.x, manager.GetComponentData<Translation>(list[i]).Value.y, 0) - new Vector3(manager.GetComponentData<Translation>(entities[j]).Value.x, manager.GetComponentData<Translation>(entities[j]).Value.y, 0)).magnitude < 2.1)
                {
                    manager.DestroyEntity(entities[j]);
                }
            }
        }
    }
}
