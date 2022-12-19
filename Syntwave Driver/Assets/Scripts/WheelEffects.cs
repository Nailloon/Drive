using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelEffects : MonoBehaviour
{

    [SerializeField] private TrailRenderer flTrail;
    [SerializeField] private TrailRenderer frTrail;
    [SerializeField] private TrailRenderer rlTrail;
    [SerializeField] private TrailRenderer rrTrail;

    [SerializeField] private WheelCollider fl;
    private WheelCollider fr;
    private WheelCollider rl;
    private WheelCollider rr;

    WheelHit wH;

    // private bool flEmitting;
    // private bool frEmitting;
    // private bool rrEmitting;
    // private bool rlEmitting;

    [SerializeField] private CarController carHandler;

    int wheelIndex = 0;

    void Start() {
        flTrail = GameObject.Find("wheel_fl_effects").GetComponent<TrailRenderer>();
        frTrail = GameObject.Find("wheel_fr_effects").GetComponent<TrailRenderer>();
        rlTrail = GameObject.Find("wheel_rl_effects").GetComponent<TrailRenderer>();
        rrTrail = GameObject.Find("wheel_rr_effects").GetComponent<TrailRenderer>();

        fl = GameObject.Find("wheel_collider_fl").GetComponent<WheelCollider>();
        fr = GameObject.Find("wheel_collider_fr").GetComponent<WheelCollider>();
        rl = GameObject.Find("wheel_collider_rl").GetComponent<WheelCollider>();
        rr = GameObject.Find("wheel_collider_rr").GetComponent<WheelCollider>();

    }

    void HandleEmitting(TrailRenderer TR, WheelCollider WC, ref int i) {
        if (!TR.emitting & WC.GetGroundHit(out wH) & Mathf.Abs(carHandler.slip[i]) > 0.4 ) {
            TR.emitting = true;
        }
        if (TR.emitting & (!WC.GetGroundHit(out wH) | Mathf.Abs(carHandler.slip[i]) <= 0.4)) TR.emitting = false;
        i += 1;
    }

    // Update is called once per frame
    void FixedUpdate() {
        HandleEmitting(flTrail, fl, ref wheelIndex);
        HandleEmitting(frTrail, fr, ref wheelIndex);
        HandleEmitting(rlTrail, rl, ref wheelIndex);
        HandleEmitting(rrTrail, rr, ref wheelIndex);
        wheelIndex = 0;
    }
}
