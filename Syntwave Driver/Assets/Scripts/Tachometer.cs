using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tachometer : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Image bar;
    private CarController targetController;
    private const float updateCooldown = 0.1f;
    private float currentUpdate;

    [SerializeField] private float currentRPM;
    [SerializeField] private float maxRPM;

    void Start()
    {
        
        targetController = target.GetComponent<CarController>();
        currentUpdate = 0f;
        maxRPM = target.GetComponent<CarController>().maxRPM;
    }

    void FixedUpdate()
    {
        bar = GetComponent<Image>();
        if (currentUpdate > updateCooldown) {

            currentRPM = target.GetComponent<CarController>().engineRPM;

            bar.GetComponent<Image>().fillAmount = currentRPM / maxRPM;
        } else {
            currentUpdate += Time.deltaTime;
        }
    }
}
