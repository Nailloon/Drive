using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Engine
{
    public float RPM;
    private float HP;
    public float currentHP(float RPM) {
        return (-RPM*RPM/maxRPM + 1.5f*RPM/maxRPM + 0.4375f)*HP;
    }
    private float currentTorque(float RPM) {
        return currentHP(RPM)*5252/RPM;
    }
    private float maxRPM;
    private float throttlePosition;
    public Engine(float hp, float mRPM) {
        RPM = 0;
        HP = hp;
        maxRPM = mRPM;
    }

    public void start() {
        RPM = Mathf.Lerp(0, 800, 1);
    }
}

public class Transmission
{
    public int currentGear;
    public float[] gearRatios;

    public Transmission(float[] gR) {
        currentGear = 0;
        gearRatios = gR;
    }
}

public class CarController : MonoBehaviour
{
    Engine engine = new Engine(150, 8000);
    Transmission transmission = new Transmission(new float[] {0.3f, 0.29f, 0.51f, 0.73f, 1f, 1.35f});

    [SerializeField] private TextMeshProUGUI RPM_UI;

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



    void Start() {
        engine.start();
        UIRPMUpdater updater = RPM_UI.GetComponent<UIRPMUpdater>();

        updater.RPM = engine.RPM;

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
            // flCollider.brakeTorque = currentBreaking;
            // frCollider.brakeTorque = currentBreaking;
            rlCollider.brakeTorque = currentBreaking;
            frCollider.brakeTorque = currentBreaking;
    }
    private void HandleSteering() {
        steerAngle = maxSteerAngle * horizontalInput;
        frCollider.steerAngle = steerAngle;
        flCollider.steerAngle = steerAngle;
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
