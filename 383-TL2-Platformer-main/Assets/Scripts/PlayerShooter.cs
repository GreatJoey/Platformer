using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private Projectile playerBulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shootCooldown = 0.2f;

    private float _nextShoot;
    private PlayerController playerController;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Time.time >= _nextShoot)
        {
            _nextShoot = Time.time + shootCooldown;
            var bullet = Instantiate(playerBulletPrefab, firePoint.position, Quaternion.identity);
            // Shoot to the right by default; swap for facing direction if you track it
            Vector2 dir = (playerController != null) ? playerController.FacingDir : Vector2.right;
            bullet.Fire(dir);
        }
    }
}
