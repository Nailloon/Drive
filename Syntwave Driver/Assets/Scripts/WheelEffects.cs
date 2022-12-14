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

    void EmitFl() {
        if (Mathf.Abs(carHandler.slip[0]) > 0.4) {
            if (flTrail.emitting) {
                if (!fl.GetGroundHit(out wH)) {
                    flTrail.emitting = false;
                }
                return;
            }
            if (flTrail.emitting) return;
            flTrail.emitting = true & fl.GetGroundHit(out wH); 
            Debug.Log("YES");
        }
        else {
            if (!flTrail.emitting) return;
            flTrail.emitting = false;
        }
    }

    void EmitFr() {
        if (Mathf.Abs(carHandler.slip[1]) > 0.4) {
            if (frTrail.emitting) {
                if (!fr.GetGroundHit(out wH)) {
                    frTrail.emitting = false;
                }
                return;
            }
            frTrail.emitting = true; 
        }
        else {
            if (!frTrail.emitting) return;
            frTrail.emitting = false;
        }
    }

    void EmitRl() {
        if (Mathf.Abs(carHandler.slip[2]) > 0.2) {
            if (rlTrail.emitting) {
                if (!rl.GetGroundHit(out wH)) {
                    rlTrail.emitting = false;
                }
                return;
            }
            if (rlTrail.emitting) return;
            rlTrail.emitting = true & rl.GetGroundHit(out wH); 
        }
        else {
            if (!rlTrail.emitting) return;
            rlTrail.emitting = false;
        }
    }

    void EmitRr() {
        if (Mathf.Abs(carHandler.slip[3]) > 0.2) {
            if (rrTrail.emitting) {
                if (!rr.GetGroundHit(out wH)) {
                    rrTrail.emitting = false;
                }
                return;
            }
            if (rrTrail.emitting) return;
            rrTrail.emitting = true  & rr.GetGroundHit(out wH); 
        }
        else {
            if (!rrTrail.emitting) return;
            rrTrail.emitting = false;
        }
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
