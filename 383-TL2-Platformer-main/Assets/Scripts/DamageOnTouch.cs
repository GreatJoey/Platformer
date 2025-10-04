using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageOnTouch : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private LayerMask targetLayers; // set to Player
    [SerializeField] private bool damageOnEnter = true;
    [SerializeField] private bool damageOnStay = false;
    [SerializeField] private float repeatRate = 0.5f;
    private float _nextDamageTime;

    private void Reset()
    {
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!damageOnEnter) return;
        TryDamage(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!damageOnStay) return;
        if (Time.time < _nextDamageTime) return;
        if (TryDamage(other))
            _nextDamageTime = Time.time + repeatRate;
    }

    private bool TryDamage(Collider2D other)
    {
        if ((targetLayers.value & (1 << other.gameObject.layer)) == 0) return false;
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(damage);
            return true;
        }
        return false;
    }
}
