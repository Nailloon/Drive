using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private GameObject cameraPoint;
    [SerializeField] private GameObject lookAtPoint;


    private void Start()
    {
        cameraPoint = GameObject.Find("camera_position");
        lookAtPoint = GameObject.Find("camera_look_point");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        follow();
    }

    private void follow() {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, cameraPoint.transform.position, Time.deltaTime * cameraSpeed);
        gameObject.transform.LookAt(lookAtPoint.gameObject.transform.position);

    }
}
