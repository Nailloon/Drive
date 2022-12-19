using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CarHealth : MonoBehaviour
{
    private static int health;
    [SerializeField] private Transform car;
    [SerializeField] private TextMeshProUGUI healthText;

    void Start()
    {
        health = 100;
        healthText.text ="Health:" + health.ToString();
    }
    private void Update()
    {
        healthText.text = "Health:" + health.ToString();
    }
    public static void TakeDamage(int damage)
    {
        health -= damage;
    }
    public static int ReturnMaxHealth()
    {
        return health;
    }
}