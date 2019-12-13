using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

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
    public Transform bulletSpawn;

    EntityManager manager;


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

        manager = World.Active.EntityManager;
    }

    // Update is called once per frame
    void Update()
    {
        MoveShip();
        KeepInBounds();
    }

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

    private void ShootBullet()
    {
        //impliment functions once nick finishes 
        Entity bullet = manager.Instantiate(bulletPre);
        manager.SetComponentData(bullet, new Translation { Value = bulletSpawn.position});
    }

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

    void OnDrawGizmosSelected()
    {
        // Draws a blue line from this transform to the target
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.up * 10);   
    }

    private void moveThing()
    {
        bulletSpawn.transform.position = new Vector3();
    }
}
