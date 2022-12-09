using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIRPMUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gui;

    public float RPM;
    void Update()
    {
        gui.text = ((int)RPM).ToString();
    }
}
