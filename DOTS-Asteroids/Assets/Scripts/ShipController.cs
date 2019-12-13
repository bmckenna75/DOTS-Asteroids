using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using UnityEngine;
using Unity.Mathematics;

public class ShipController : MonoBehaviour
{
    Rigidbody rigidbodyRef;

    //camera variables
    Camera camera;
    float leftConstraint;
    float rightConstraint;
    float topConstraint;
    float bottomConstraint;
    float distanceZ;
    public float buffer;

    public GameObject bulletPre;
    private Entity prefab;
    public Transform bulletSpawn;
    public int bulletCount;

    EntityManager manager;
    bool shootMany;
    public GameObject RotateSlave;

    public NativeArray<Entity> bullets;
    [SerializeField]
    public List<Entity> entities;
    // Start is called before the first frame update
    void Start()
    {
        rigidbodyRef = this.GetComponent<Rigidbody>();

        //set Camera Variables
        camera = Camera.main;
        distanceZ = Mathf.Abs(camera.transform.position.z + transform.position.z);
        leftConstraint = camera.ScreenToWorldPoint(new Vector3(0f, 0f, distanceZ)).x;
        rightConstraint = camera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, distanceZ)).x;
        topConstraint = camera.ScreenToWorldPoint(new Vector3(0f, Screen.height, distanceZ)).y;
        bottomConstraint = camera.ScreenToWorldPoint(new Vector3(0f, 0f, distanceZ)).y;
        buffer = 1.2f;

        prefab = Unity.Entities.GameObjectConversionUtility.ConvertGameObjectHierarchy(bulletPre, World.Active);
        manager = World.Active.EntityManager;
        bulletCount = 50;
        shootMany = false;
        entities = new List<Entity>();
    }

    public List<Entity> GetEntities()
    {
        return entities;
    }

    // Update is called once per frame
    void Update()
    {
        MoveShip();
        KeepInBounds();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBullet();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (shootMany)
            {
                shootMany = false;
                return;
            }
            else
            {
                shootMany = true;
            }
        }
    }
    //applys forces to the ship's ridged body
    private void MoveShip()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rigidbodyRef.AddForce(transform.up * Time.deltaTime * 70);
        }
        if (Input.GetKey(KeyCode.A))
        {
            //rigidbodyRef.AddForce(new Vector3(-1.0f, 0.0f, 0.0f));
            transform.Rotate(Vector3.forward * Time.deltaTime * 100);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rigidbodyRef.AddForce(-transform.up * Time.deltaTime * 20);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //rigidbodyRef.AddForce(new Vector3(1.0f, 0.0f, 0.0f));
            transform.Rotate(-Vector3.forward * Time.deltaTime * 100);
        }
    }
    //instanciates bullets
    private void ShootBullet()
    {
        //instanciates a lot of bullets
        if (shootMany)
        {
            Vector3 tempRot = transform.rotation.eulerAngles;
            int max = bulletCount / 2;
            int min = -max;
            int index = 0;

            bullets = new NativeArray<Entity>(bulletCount, Allocator.Temp);
            manager.Instantiate(prefab, bullets);

            for (int i = min; i < max; i++)
            {
                tempRot.z = (transform.rotation.z + bulletCount/360 * i) % 360;
                RotateSlave.transform.rotation = Quaternion.Euler(tempRot) * Quaternion.Euler(transform.up);

                manager.SetComponentData<Translation>(bullets[index], new Translation { Value = bulletSpawn.position});
                manager.SetComponentData<BulletData>(bullets[index], new BulletData { velocity = (RotateSlave.transform.up * Time.deltaTime * 300), rotation = Quaternion.Euler(tempRot) });
                entities.Add(bullets[index]);
                index++;
            }

            bullets.Dispose();
        }
        //instanciates a normal amount of bullets
        else
        {
            Entity bullet = manager.Instantiate(prefab);
            entities.Add(bullet);
            manager.SetComponentData<Translation>(bullet, new Translation { Value = bulletSpawn.position });
            manager.SetComponentData<BulletData>(bullet, new BulletData { velocity = (transform.up * Time.deltaTime * 300), rotation = transform.rotation });
        }
    }
    //loops the ship on the screen
    private void KeepInBounds()
    {
        if (transform.position.x <= leftConstraint - buffer)
        {
            transform.position = new Vector3(rightConstraint + (buffer/2), transform.position.y, transform.position.z);
        }
        if (transform.position.x >= rightConstraint + buffer)
        {
            transform.position = new Vector3(leftConstraint - (buffer/2), transform.position.y, transform.position.z);
        }
        if (transform.position.y >= topConstraint + buffer)
        {
            transform.position = new Vector3(transform.position.x, bottomConstraint - (buffer/2), transform.position.z);
        }
        if (transform.position.y <= bottomConstraint - buffer)
        {
            transform.position = new Vector3(transform.position.x, topConstraint + (buffer/2), transform.position.z);
        }
    }
}
