using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brakelights : MonoBehaviour
{
    private MeshRenderer brakelightsRender;
    void Start()
    {
        brakelightsRender = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        
    }

    public void change() {
        brakelightsRender.enabled = !brakelightsRender.enabled;
    }
}
