using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private GameObject cameraPoint;
    [SerializeField] private GameObject lookAtPoint;

    private CarController targetController;


    private void Start()
    {
        cameraPoint = GameObject.Find("camera_position");
        lookAtPoint = GameObject.Find("camera_look_point");
        targetController = target.GetComponent<CarController>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        cameraPoint.transform.position = target.transform.position - 4.5f * targetController.physicsBody.velocity.normalized + 1.9f * Vector3.up;
        cameraSpeed = target.GetComponent<Rigidbody>().velocity.magnitude;
        Camera.main.fieldOfView = 46 + Mathf.Clamp(target.GetComponent<Rigidbody>().velocity.magnitude, 0, 20);
        follow();
    }

    private void follow() {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, cameraPoint.transform.position, Time.deltaTime * cameraSpeed);
        gameObject.transform.LookAt(lookAtPoint.gameObject.transform.position);

    }
}