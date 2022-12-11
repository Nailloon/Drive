using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private Rigidbody body;
    [SerializeField] private TextMesh speedElement;
    private const float updateCooldown = 0.1f;
    private float currentUpdate;

    void Start()
    {
        body = target.GetComponent<Rigidbody>();
        speedElement = GetComponent<TextMesh>();
        currentUpdate = 0f;
    }

    void FixedUpdate()
    {
        if (currentUpdate > updateCooldown) {
            speedElement.text = ((int)(Mathf.Pow(Mathf.Pow(body.velocity.x, 2) + Mathf.Pow(body.velocity.z, 2), 0.5f) * 2.237f)).ToString() + " M/H";
            currentUpdate = 0;
        } else {
            currentUpdate += Time.deltaTime;
        }
    }

}
