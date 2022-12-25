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

    private ParticleSystem flSmoke;
    private ParticleSystem frSmoke;
    private ParticleSystem rlSmoke;
    private ParticleSystem rrSmoke;


    WheelHit wH;

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

        flSmoke = GameObject.Find("wheel_fl_effects").GetComponent<ParticleSystem>();
        frSmoke = GameObject.Find("wheel_fr_effects").GetComponent<ParticleSystem>();
        rlSmoke = GameObject.Find("wheel_rl_effects").GetComponent<ParticleSystem>();
        rrSmoke = GameObject.Find("wheel_rr_effects").GetComponent<ParticleSystem>();
    }

    void HandleEmitting(TrailRenderer TR, WheelCollider WC, ref int i) {
        if (!TR.emitting & WC.GetGroundHit(out wH) & Mathf.Abs(carHandler.slip[i]) > 0.4 ) {
            TR.emitting = true;
            StartSmoke();
        }
        if (TR.emitting & (!WC.GetGroundHit(out wH) | Mathf.Abs(carHandler.slip[i]) <= 0.4)) {
            TR.emitting = false;
            StopSmoke();
        }
        i += 1;
    }

    void HandleSmoke() {

    }

    // Update is called once per frame
    void FixedUpdate() {
        HandleEmitting(flTrail, fl, ref wheelIndex);
        HandleEmitting(frTrail, fr, ref wheelIndex);
        HandleEmitting(rlTrail, rl, ref wheelIndex);
        HandleEmitting(rrTrail, rr, ref wheelIndex);
        wheelIndex = 0;
    }

    void StartSmoke() {
        flSmoke.Play();
        frSmoke.Play();
        rlSmoke.Play();
        rrSmoke.Play();
    }

    void StopSmoke() {
        flSmoke.Stop();
        frSmoke.Stop();
        rlSmoke.Stop();
        rrSmoke.Stop();
    }
}
