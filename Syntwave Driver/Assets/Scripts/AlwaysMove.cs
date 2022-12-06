using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysMove : MonoBehaviour
{
    public float speed = 1500;

    void LateUpdate()
    {
        transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
    }
}