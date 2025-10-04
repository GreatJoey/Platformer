using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerupEffect PowerupEffect;

    [SerializeField] private SMScript soundManager;

    private void Start()
    {
        soundManager = FindObjectOfType<SMScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);

            soundManager.PowerupSound();

            PowerupEffect.Apply(collision.gameObject);
        }
    }
}
 