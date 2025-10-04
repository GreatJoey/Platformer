using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/SpeedBuff")]
public class SpeedBuff : PowerupEffect
{
    public float multiplier = 2f;
    public float duration = 5f;
    public override void Apply(GameObject target)
    {
        PlayerController pc = target.GetComponent<PlayerController>();
        
        if (pc != null )
        {
            pc.ApplySpeedBuff(multiplier, duration);
        }
    }
}