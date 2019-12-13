using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class BulletManager : MonoBehaviour
{
    EntityManager manager;
    [SerializeField]
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        manager = World.Active.EntityManager;
    }


    // Update is called once per frame
    void Update()
    {

    }

    void SpawnBulletECS(Vector3 rotation)
    {
        Entity bullet = manager.Instantiate(bulletPrefab);
        //manager.SetComponentData(asteroid, new Position)
    }
}
