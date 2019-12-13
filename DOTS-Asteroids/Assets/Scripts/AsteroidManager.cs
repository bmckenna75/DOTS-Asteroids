using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    EntityManager manager;
    [SerializeField]
    public GameObject AsteroidPrefab;
    private Entity asteroidPrefab;
    Random random;
    List<Entity> entities;
    private static bool first;

    // Start is called before the first frame update
    void Awake()
    {
        manager = World.Active.EntityManager;
        random = new Random();
        asteroidPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(AsteroidPrefab, World.Active);
        first = true;
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
}
