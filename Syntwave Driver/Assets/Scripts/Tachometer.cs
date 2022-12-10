using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tachometer : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private CarController targetController;
    private TextMeshProUGUI guiElement;
    void Start()
    {
        guiElement = GetComponent<TextMeshProUGUI>();
        targetController = target.GetComponent<CarController>();
    }

    void Update()
    {
        guiElement.text = ((int)targetController.engineRPM).ToString() + " R/M";
    }
}
