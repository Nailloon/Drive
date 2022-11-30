using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysMove : MonoBehaviour
{
    public float speed = 15;
    void Start()
    {
        
    }

    void Update()
    {
        //Движение с постоянной скоростью вперед
        transform.Translate(Vector3.right* speed * Time.deltaTime);
    }
}
