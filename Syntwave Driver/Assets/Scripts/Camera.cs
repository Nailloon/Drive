using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private GameObject cameraPoint;
    [SerializeField] private GameObject lookAtPoint;
    void Awake()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        follow();
    }

    private void follow() {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, cameraPoint.transform.position, Time.deltaTime * cameraSpeed);
        gameObject.transform.LookAt(lookAtPoint.gameObject.transform.position);
    }
}
