using UnityEngine;

[CreateAssetMenu(menuName = "Powerup/Bigbuff")]
public class Bugbuff : PowerupEffect
{
    [SerializeField] private float multiplier = 2f;
    [SerializeField] private float duration = 5f;
    [SerializeField] private float increase_duration = 1f;

    public override void Apply(GameObject target)
    {
        PlayerController pc = target.GetComponent<PlayerController>();

        if (pc != null)
        {
            pc.ApplyBigBuff(multiplier, duration, increase_duration);
        }
    }
}
