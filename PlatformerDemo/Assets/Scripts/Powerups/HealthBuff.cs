using UnityEngine;

[CreateAssetMenu(menuName = "Powerup/HealthBuff")]
public class HealthBuff : PowerupEffect
{
    [SerializeField] private int amount;

    public override void Apply(GameObject target)
    {
        target.GetComponent<Health>().Heal(amount);
    }
}
