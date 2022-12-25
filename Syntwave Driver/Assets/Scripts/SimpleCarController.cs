using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour
{
    public float speed = 4;
    public float steeringSpeed = 4;
    private const float gravity = 5f;
    public Vector3 moving;
    public Vector3 steering;
    // Start is called before the first frame update
    void Start()
    {
        moving = Vector3.zero;   
    }

    // Update is called once per frame
    void Update()
    {
        Movement();   
    }
    void Movement()
    {
            // if (collision.gameObject.tag == "Terrain") {
            moving = new Vector3(0f, 0f, Input.GetAxis("Vertical"));
            moving *= speed;
            steering = new Vector3(0f, Input.GetAxis("Horizontal"));
            steering *= steeringSpeed;
            // }
            // moving.y -= gravity;
            transform.Translate(moving * Time.deltaTime);
            transform.Rotate(steering * Time.deltaTime);
    }
}
