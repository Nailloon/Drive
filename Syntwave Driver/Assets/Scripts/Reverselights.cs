using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reverselights : MonoBehaviour
{
    private MeshRenderer reverselightsRender;
    void Start()
    {
        reverselightsRender = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        
    }

    public void on() {
        reverselightsRender.enabled = true;
    }
    public void off() {
        reverselightsRender.enabled = false;
    }
}
