using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/BigBuff")]
public class BigBuff : PowerupEffect
{
    public float multiplier = 2f;
    public float duration = 5f;
    public float increaseduration = 1f;
    public override void Apply(GameObject target)
    {
        PlayerController pc = target.GetComponent<PlayerController>();

        if (pc != null )
        {
            pc.ApplyBigBuff(multiplier, duration, increaseduration);
        }
    }
}