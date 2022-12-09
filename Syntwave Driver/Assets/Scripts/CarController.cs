using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CarController : MonoBehaviour
{

    [SerializeField] private float horizontalInput;
    [SerializeField] private float verticalInput;
    [SerializeField] private float steerAngle;
    [SerializeField] private bool isBreaking;
    [SerializeField] private float currentBreaking;

    [SerializeField] public float motorForce;
    [SerializeField] public float breakingForce;
    [SerializeField] public float maxSteerAngle;
    
    [SerializeField] private WheelCollider flCollider;
    [SerializeField] private WheelCollider frCollider;
    [SerializeField] private WheelCollider rlCollider;
    [SerializeField] private WheelCollider rrCollider;

    [SerializeField] private Transform flTransform;
    [SerializeField] private Transform frTransform;
    [SerializeField] private Transform rlTransform;
    [SerializeField] private Transform rrTransform;

    [SerializeField] private brakelights brakelights;

    [SerializeField] private float radius;

    [SerializeField] private float wheelBase;

    private void Start() {

    }

    private void FixedUpdate() {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }
 
    private void HandleMotor() {
        frCollider.motorTorque = verticalInput * motorForce;
        flCollider.motorTorque = verticalInput * motorForce;
        // rrCollider.motorTorque = verticalInput * motorForce;
        // rrCollider.motorTorque = verticalInput * motorForce;
        currentBreaking = isBreaking ? breakingForce : 0f;
        Brake();
    }
    private void Brake() {
        flCollider.brakeTorque = currentBreaking;
        frCollider.brakeTorque = currentBreaking;
        rlCollider.brakeTorque = currentBreaking;
        frCollider.brakeTorque = currentBreaking;
        if (currentBreaking > 0) brakelights.change();

    }
    private void HandleSteering() {
        steerAngle = horizontalInput;
        if (steerAngle > 0) {
            frCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (radius + (1.5f / 2))) * horizontalInput;
            flCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (radius -(1.5f / 2))) * horizontalInput;
        } else {
            frCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (radius - (1.5f / 2))) * horizontalInput;
            flCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (radius + (1.5f / 2))) * horizontalInput;
        }
        
    }
    private void UpdateWheels() {
        UpdateWheel(flCollider, flTransform);
        UpdateWheel(frCollider, frTransform);
        UpdateWheel(rlCollider, rlTransform);
        UpdateWheel(rrCollider, rrTransform);

    }
    private void UpdateWheel(WheelCollider col, Transform tform) {
        Vector3 pos;
        Quaternion rot;
        col.GetWorldPose(out pos, out rot);
        tform.position = pos;
        tform.rotation = rot;
    }
    private void GetInput() {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBreaking = Input.GetKey(KeyCode.Space);
    }
}
