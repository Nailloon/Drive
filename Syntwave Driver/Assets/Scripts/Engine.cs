using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Engine : MonoBehaviour
{
    public bool isRunning;
    public bool isStarting;
    [SerializeField] public float RPM;
    [SerializeField] private float HP;
    public float currentHP(float RPM) {
        return (-RPM*RPM/maxRPM + 1.5f*RPM/maxRPM + 0.4375f)*HP;
    }
    private float currentTorque(float RPM) {
        return currentHP(RPM)*5252/RPM;
    }
    [SerializeField] private float maxRPM;
    [SerializeField] private UIRPMUpdater Engine_UI_updater;
    [SerializeField] private Transmission transmission;
    private float throttlePosition;
    [SerializeField] private float timeElapsedDuringStart = 0;
    private int stageOfStart = 0;
    public Engine(float hp, float mRPM) {
        RPM = 0;
        HP = hp;
        maxRPM = mRPM;
    }
    private void Update() {
        if (!isRunning && Input.GetKeyDown(KeyCode.N)) {
            isStarting = true;
        }
        if (isStarting) {
            float startDuration = 5;
            float x = timeElapsedDuringStart / startDuration;
            if (x <= 1) {
                RPM = RPM + 1000*Time.deltaTime * (3*Mathf.Pow(x, 2) - 4.5f*x + 1.5f);
                timeElapsedDuringStart += Time.deltaTime;
            }
        } else {
            isStarting = false;
        }
        
    } 

}
