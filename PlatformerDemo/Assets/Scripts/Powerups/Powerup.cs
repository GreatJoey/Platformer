using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private PowerupEffect powerupEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);

            powerupEffect.Apply(collision.gameObject);
        }
    }
}
