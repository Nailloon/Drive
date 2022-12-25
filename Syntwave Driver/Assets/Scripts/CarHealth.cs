using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CarHealth : MonoBehaviour
{
    [SerializeField] private static int health;
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
        Debug.Log(damage);
        health -= damage;
        if (health <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }
    public static int ReturnMaxHealth()
    {
        return health;
    }
}