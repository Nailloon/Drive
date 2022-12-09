using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CarController : MonoBehaviour
{

    [SerializeField] private float horizontalInput;
    [SerializeField] private float verticalInput;
    [SerializeField] private float steerAngle;
    private bool isBreaking;
    private bool isHandBraking;
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

    [SerializeField] private GameObject brakelights;

    private brakelights brakelightsHandle;

    private MeshRenderer brakelightsRender;

    [SerializeField] private float radius;

    [SerializeField] private float wheelBase;

    [SerializeField] private float downForce;
    
    [SerializeField] private GameObject centerOfMassPoint;

    private Rigidbody physicsBody;

    private void Start() {
        physicsBody = GetComponent<Rigidbody>();
        brakelightsHandle = brakelights.GetComponent<brakelights>();
        brakelightsRender = brakelights.GetComponent<MeshRenderer>();
    }

    private void FixedUpdate() {
        calculateCenterOfMass();
        applyDownForce();
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }
 
    private void HandleMotor() {
        frCollider.motorTorque = verticalInput * motorForce;
        flCollider.motorTorque = verticalInput * motorForce;
        rrCollider.motorTorque = verticalInput * motorForce;
        rlCollider.motorTorque = verticalInput * motorForce;
        currentBreaking = (isBreaking | isHandBraking) ? breakingForce : 0f;
        HandBrake();
        Brake();
    }
    private void Brake() {
        flCollider.brakeTorque = currentBreaking;
        frCollider.brakeTorque = currentBreaking;
        rlCollider.brakeTorque = currentBreaking;
        rrCollider.brakeTorque = currentBreaking;
        if (isBreaking & !(brakelightsRender.enabled)) {
            brakelightsHandle.change();
        }
        if (!isBreaking & brakelightsRender.enabled) {
            brakelightsHandle.change();
        }

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
        isBreaking = (verticalInput < 0) & (physicsBody.velocity.magnitude > 0.1f);
        isHandBraking = Input.GetKey(KeyCode.Space);
    }

    private void calculateCenterOfMass() {
        physicsBody.centerOfMass = centerOfMassPoint.transform.localPosition;
    }

    private void applyDownForce() {
        physicsBody.AddForce(-transform.up * downForce * physicsBody.velocity.magnitude);
    }
    
    private void HandBrake() {
        rlCollider.brakeTorque = currentBreaking / 2;
        rrCollider.brakeTorque = currentBreaking / 2;
    }
}
