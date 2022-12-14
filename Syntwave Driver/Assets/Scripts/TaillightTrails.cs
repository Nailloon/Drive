using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaillightTrails : MonoBehaviour
{
    [SerializeField] private AnimationCurve onBreaking;
    [SerializeField] private AnimationCurve notBreaking;
    private TrailRenderer TR;
    [SerializeField] private CarController target;
    void Start()
    {
        TR = GetComponent<TrailRenderer>();
    }

    void FixedUpdate() {
        HandleEmitting();
    }

    void HandleEmitting() {
        if (!TR.emitting & !target.reverseGear) {
            TR.emitting = true;
        }
        if (TR.emitting & (target.reverseGear | target.physicsBody.velocity.magnitude < 10)) {
            TR.emitting = false;
            return;
        }
        if (target.isBreaking) {
            TR.widthMultiplier = 3.5f;
        } else TR.widthMultiplier = 1;
    }
}
