using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{

    Vector2 posVec;
    Vector2 velocityVec;
    Rigidbody rigidbodyRef;


    // Start is called before the first frame update
    void Start()
    {
        rigidbodyRef = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveShip();
    }

    private void MoveShip()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rigidbodyRef.AddForce(new Vector3(0.0f, 1.0f, 0.0f));
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigidbodyRef.AddForce(new Vector3(-1.0f, 0.0f, 0.0f));
        }
        if (Input.GetKey(KeyCode.S))
        {
            rigidbodyRef.AddForce(new Vector3(0.0f, -1.0f, 0.0f));
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigidbodyRef.AddForce(new Vector3(1.0f, 0.0f, 0.0f));

        }
    }

    private void ShootBullet()
    {
        //impliment functions once nick finishes 
    }

}
