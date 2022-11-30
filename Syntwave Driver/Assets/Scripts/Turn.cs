using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    public float rotatespeed=25;
    private float horInput;
    void Start()
    {
        
    }

    void Update()
    {
        horInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * rotatespeed * Time.deltaTime * horInput);
    }
}
