using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float lifeSeconds = 5f;
    [SerializeField] private LayerMask damageLayers; // set to Player
    [SerializeField] private bool useRbMovement = false;

    private Vector2 _direction;
    private float _deathTime;
    private Rigidbody2D _rb;

    public void Fire(Vector2 direction)
    {
        _direction = direction.normalized;
        _deathTime = Time.time + lifeSeconds;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (_rb != null)
        {
            _rb.isKinematic = true;
            _rb.gravityScale = 0f;
            _rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }
    }

    private void Update()
    {
        if (Time.time >= _deathTime) Destroy(gameObject);

        if (useRbMovement && _rb != null)
            _rb.MovePosition(_rb.position + _direction * speed * Time.deltaTime);
        else
            transform.Translate(_direction * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((damageLayers.value & (1 << other.gameObject.layer)) == 0) return;

        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void Reset()
    {
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }
}
