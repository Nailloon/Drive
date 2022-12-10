using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Transform car;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Update()
    {
        scoreText.text = ((int)(car.position.z / 2)*(-1)).ToString();
    }
}
