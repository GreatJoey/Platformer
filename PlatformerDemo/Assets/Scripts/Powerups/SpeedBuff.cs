using UnityEngine;

[CreateAssetMenu(menuName = "Powerup/SpeedBuff")]
public class SpeedBuff : PowerupEffect
{
    [SerializeField] private float multiplier = 2f;
    [SerializeField] private float duration = 5f;

    public override void Apply(GameObject target)
    {
        PlayerController pc = target.GetComponent<PlayerController>();

        if (pc != null)
        {
            pc.ApplySpeedBuff(multiplier, duration);
        }
    }
}
