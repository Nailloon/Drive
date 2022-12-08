using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmission : MonoBehaviour
{
    public int currentGear;
    [SerializeField] public float[] gearRatios;
    // Transmission transmission = new Transmission(new float[] {0.3f, 0.29f, 0.51f, 0.73f, 1f, 1.35f});
    public int[] gears = {-1, 0, 1, 2, 3, 4, 5};
    private void Update() {

    }
}
