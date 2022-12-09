using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private Rigidbody body;
    private TextMeshProUGUI speedElement;
    void Start()
    {
        body = target.GetComponent<Rigidbody>();
        speedElement = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        speedElement.text = ((int)(Mathf.Pow(Mathf.Pow(body.velocity.x, 2) + Mathf.Pow(body.velocity.z, 2), 0.5f) * 2.237f)).ToString() + " M/H";
    }
}
