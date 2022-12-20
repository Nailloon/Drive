using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldFollows : MonoBehaviour
{

    [SerializeField] private GameObject target;
    [SerializeField] GameObject road;
    private float x_coordinate;
    private float y_coordinate;
    void Start()
    {
        x_coordinate = road.transform.position.z;
        y_coordinate = target.transform.position.y - 3;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(x_coordinate, y_coordinate, target.transform.position.z);
    }
}
