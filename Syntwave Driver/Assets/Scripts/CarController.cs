using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CarController : MonoBehaviour
{

    [SerializeField] public float horizontalInput;
    [SerializeField] public float verticalInput;
    private float steerAngle;
    [SerializeField] public bool isBreaking;
    private bool isHandBraking;
    private float currentBreaking;

    [SerializeField] public float motorForce;
    [SerializeField] public float breakingForce;
    [SerializeField] public float maxSteerAngle;

    [SerializeField] AnimationCurve enginePower;
    
    [SerializeField] private WheelCollider flCollider;
    [SerializeField] private WheelCollider frCollider;
    [SerializeField] private WheelCollider rlCollider;
    [SerializeField] private WheelCollider rrCollider;

    private WheelFrictionCurve flSidewaysFriction;
    private WheelFrictionCurve frSidewaysFriction;
    private WheelFrictionCurve rlSidewaysFriction;
    private WheelFrictionCurve rrSidewaysFriction;

    private WheelFrictionCurve flForwardFriction;
    private WheelFrictionCurve frForwardFriction;
    private WheelFrictionCurve rlForwardFriction;
    private WheelFrictionCurve rrForwardFriction;

    private WheelFrictionCurve roadForward;
    private WheelFrictionCurve roadSideways;

    WheelHit wH;

    public float[] slip = new float[4];

    private Transform flTransform;
    private Transform frTransform;
    private Transform rlTransform;
    private Transform rrTransform;

    [SerializeField] private GameObject brakelights;
    private brakelights brakelightsHandle;
    private MeshRenderer brakelightsRender;

    [SerializeField] private GameObject reverselights;
    private Reverselights reverselightsHandle;
    private MeshRenderer reverselightsRender;

    [SerializeField] private float radius;

    [SerializeField] private float wheelBase;

    [SerializeField] private float downForce;
    
    private GameObject centerOfMassPoint;

    public Rigidbody physicsBody;

    public float engineRPM;
    [SerializeField] public float maxRPM;
    [SerializeField] private float engineCutOff;

    [SerializeField] private float[] gearRatios;
    
    [SerializeField] public int currentGear;

    [SerializeField] private int[] upshiftPoints;
    [SerializeField] private int[] downshiftPoints;

    private const float shiftDuration = 0.4f;
    [SerializeField] private float currentShiftDuration = 0;

    private bool needToUpshift = false;
    private bool needToDownshift = false;

    [SerializeField] public bool reverseGear;

    private const float upshiftCooldown = 1;
    private const float downshiftCooldown = 1;
    private float currentUpshiftTime = 0;


    private void Start() {
        coverageSetUp();
        VehicleSetUp();
    }

    private void FixedUpdate() {
        calculateCenterOfMass();
        applyDownForce();
        GetInput();
        calculateEnginePower();
        goReverse();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        GetFriction();
    }
    
    private void VehicleSetUp() {

        physicsBody = GetComponent<Rigidbody>();

        brakelights = GameObject.Find("brakelights"); 
        brakelightsHandle = brakelights.GetComponent<brakelights>();
        brakelightsRender = brakelights.GetComponent<MeshRenderer>();

        reverselights = GameObject.Find("reverselights"); 
        reverselightsHandle = reverselights.GetComponent<Reverselights>();

        flCollider = GameObject.Find("wheel_collider_fl").GetComponent<WheelCollider>();
        frCollider = GameObject.Find("wheel_collider_fr").GetComponent<WheelCollider>();
        rlCollider = GameObject.Find("wheel_collider_rl").GetComponent<WheelCollider>();
        rrCollider = GameObject.Find("wheel_collider_rr").GetComponent<WheelCollider>();

        flCollider.forwardFriction = roadForward;
        frCollider.forwardFriction = roadForward;
        rlCollider.forwardFriction = roadForward;
        rrCollider.forwardFriction = roadForward;

        flCollider.sidewaysFriction = roadSideways;
        frCollider.sidewaysFriction = roadSideways;
        rlCollider.sidewaysFriction = roadSideways;
        rrCollider.sidewaysFriction = roadSideways;

        flForwardFriction = flCollider.forwardFriction;
        frForwardFriction = frCollider.forwardFriction;
        rlForwardFriction = rlCollider.forwardFriction;
        rrForwardFriction = rrCollider.forwardFriction;

        flSidewaysFriction = flCollider.sidewaysFriction;
        frSidewaysFriction = frCollider.sidewaysFriction;
        rlSidewaysFriction = rlCollider.sidewaysFriction;
        rrSidewaysFriction = rrCollider.sidewaysFriction;

        flTransform = GameObject.Find("wheel_fl_axis").transform;
        frTransform = GameObject.Find("wheel_fr_axis").transform;
        rlTransform = GameObject.Find("wheel_rl_axis").transform;
        rrTransform = GameObject.Find("wheel_rr_axis").transform;

        centerOfMassPoint = GameObject.Find("center_mass");

        gearRatios = new float[] {3.33f, 3.45f, 1.94f, 1.36f, 0.97f, 0.73f};
        upshiftPoints = new int[] {100000, 6500, 6200, 6300, 6300, -1};
        downshiftPoints = new int[] {-1, 0, 1750, 3000, 3520, 3700};


        engineRPM = 200;
    }

    private void coverageSetUp() {
        roadForward.extremumSlip = 0.4f;
        roadForward.extremumValue = 1f;
        roadForward.asymptoteSlip = 0.8f;
        roadForward.asymptoteValue = 0.5f;
        roadForward.stiffness = 1.5f;

        roadSideways.extremumSlip = 0.2f;
        roadSideways.extremumValue = 0.5f;
        roadSideways.asymptoteSlip = 0.5f;
        roadSideways.asymptoteValue = 0.75f;
        roadSideways.stiffness = 1.4f;
    }
 
    private void HandleMotor() {
        frCollider.motorTorque = verticalInput * motorForce / 4;
        flCollider.motorTorque = verticalInput * motorForce / 4;
        rrCollider.motorTorque = verticalInput * motorForce / 4;
        rlCollider.motorTorque = verticalInput * motorForce / 4;
        physicsBody.drag = reverseGear ? 0.5f : 0.25f*(1 - Mathf.Abs(verticalInput));
        currentBreaking = isBreaking ? breakingForce : 0f;
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
        if (!reverseGear) {
            isBreaking = (verticalInput < 0) & (physicsBody.velocity.magnitude > 0.2f);
        } else isBreaking = ((verticalInput > 0) & (physicsBody.velocity.magnitude > 0.2f));
        isHandBraking = Input.GetKey(KeyCode.Space);
    }

    private void calculateCenterOfMass() {
        physicsBody.centerOfMass = centerOfMassPoint.transform.localPosition;
    }

    private void applyDownForce() {
        physicsBody.AddForce(-transform.up * downForce * physicsBody.velocity.magnitude);
    }
    
    private void HandBrake() {
        rlCollider.brakeTorque = breakingForce / 2;
        rrCollider.brakeTorque = breakingForce / 2;
        skid();
    }
    private void GetFriction() {
        flCollider.GetGroundHit(out wH);
        slip[0] = wH.sidewaysSlip;
        frCollider.GetGroundHit(out wH);
        slip[1] = wH.sidewaysSlip;
        rlCollider.GetGroundHit(out wH);
        slip[2] = wH.sidewaysSlip;
        rrCollider.GetGroundHit(out wH);
        slip[3] = wH.sidewaysSlip;
    }

    private float wheelsRPM() {
        return (flCollider.rpm + flCollider.rpm + flCollider.rpm + flCollider.rpm) / 4;
    }

    private void upshift() {
        currentShiftDuration += Time.deltaTime;
        if (currentShiftDuration < shiftDuration) {
            motorForce = 0;
            flCollider.brakeTorque = 2 * breakingForce;
            frCollider.brakeTorque = 2 * breakingForce;
            rlCollider.brakeTorque = 2 * breakingForce;
            rrCollider.brakeTorque = 2 * breakingForce;
        } else {
            currentBreaking = 0;
            currentShiftDuration = 0;
            currentGear = currentGear == gearRatios.Length - 1 ? currentGear : currentGear + 1;
            currentUpshiftTime = 0;
            needToUpshift = false;
        }
    }

    private void downshift() {
        currentShiftDuration += Time.deltaTime;
        if (currentShiftDuration < shiftDuration) {
            motorForce = 0;
        } else {
            currentBreaking = 0;
            currentShiftDuration = 0;
            currentGear = currentGear == 1 ? currentGear : currentGear - 1;
            needToDownshift = false;
        }

    }

    private void goReverse() {
        if (physicsBody.velocity.magnitude <= 0.1f & verticalInput < 0) {
            reverseGear = true;
        }
        if (physicsBody.velocity.magnitude <= 0.1f & verticalInput > 0) {
            reverseGear = false;
        }
        if (reverseGear) reverselightsHandle.on();
        else reverselightsHandle.off();
    }

    private void calculateEnginePower() {
        motorForce = isGrounded() ? enginePower.Evaluate(engineRPM) * gearRatios[currentGear] : 0;
        float velocity = 0.0f;
        currentUpshiftTime += Time.deltaTime;
        engineRPM = Mathf.SmoothDamp(engineRPM, (Mathf.Abs(wheelsRPM()) * 4.4f *  gearRatios[currentGear]), ref velocity, 0.1f);
        if (engineRPM < 650) {
            engineRPM = 650;
            currentGear = 1;
        }
        if (engineRPM > upshiftPoints[currentGear] & !reverseGear) {
            needToUpshift = true;
        }
        if(engineRPM > engineCutOff) {
            // engineRPM = engineCutOff;
            // engineRPM = Mathf.SmoothDamp(engineRPM, engineCutOff - 1000, ref velocity, 0.1f);
        }
        if (needToUpshift & currentUpshiftTime > upshiftCooldown) {
            upshift();
        }
        if (engineRPM < downshiftPoints[currentGear]) {
            needToDownshift = true;
        }
        if (needToDownshift) {
            downshift();
        } 
    }

    bool isGrounded() {
        return flCollider.GetGroundHit(out wH) | frCollider.GetGroundHit(out wH) | rlCollider.GetGroundHit(out wH) | rrCollider.GetGroundHit(out wH);
    }

    void skid() {
        if (!isHandBraking) {
            flCollider.forwardFriction = roadForward;
            frCollider.forwardFriction = roadForward;
            rlCollider.forwardFriction = roadForward;
            rrCollider.forwardFriction = roadForward;

            flCollider.sidewaysFriction = roadSideways;
            frCollider.sidewaysFriction = roadSideways;
            rlCollider.sidewaysFriction = roadSideways;
            rrCollider.sidewaysFriction = roadSideways;
        } else {
            rlForwardFriction = rlCollider.forwardFriction;
            rlSidewaysFriction = rlCollider.sidewaysFriction;
            rrForwardFriction = rrCollider.forwardFriction;
            rrSidewaysFriction = rrCollider.sidewaysFriction;

            rlForwardFriction.stiffness = 0.75f;
            rlSidewaysFriction.stiffness = 0.35f;
            rrForwardFriction.stiffness = 0.75f;
            rrSidewaysFriction.stiffness = 0.35f;

            rlCollider.forwardFriction = rlForwardFriction;
            rlCollider.sidewaysFriction = rlSidewaysFriction;
            rrCollider.forwardFriction = rrForwardFriction;
            rrCollider.sidewaysFriction = rrSidewaysFriction;
        }
    }
}
