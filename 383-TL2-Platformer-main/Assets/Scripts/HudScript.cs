using UnityEngine;
using UnityEngine.UI;

public class HudScript : MonoBehaviour
{

    [SerializeField] private Slider healthBarAmount;
    [SerializeField] private GameObject player;

    private Health playerHealth;

    void Start()
    {
        playerHealth = player.GetComponent<Health>();

        healthBarAmount.maxValue = playerHealth.maxHealth;
        healthBarAmount.value = playerHealth.currentHealth;
    }

    void Update()
    {
        healthBarAmount.value = playerHealth.currentHealth;
    }
}
