using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTagManager : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        switch (col.gameObject.tag)
        {
            case "Light":
                CarHealth.TakeDamage((int)(2 * rb.velocity.magnitude) / 2);
                break;
            case "Fencing":
                CarHealth.TakeDamage((int)(rb.velocity.magnitude) / 3);
                break;
            case "Heavy":
                CarHealth.TakeDamage((int)(4 * rb.velocity.magnitude) / 2);
                break;
            case "Deadly":
                CarHealth.TakeDamage(CarHealth.ReturnMaxHealth());
                break;
        }
    }
}
