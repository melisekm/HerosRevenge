using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiProjectileAbility : RangedAbility
{
    // public float projectileSpread = 60f;
    public int numberOfProjectiles = 4;

    public override void Activate(AbilityStats stats, Vector3 target, Faction targetFaction)
    {
        base.Activate(stats, target, targetFaction);
        // apply linear regression to calculate the spread of the projectiles based on distance from mouse
        // target is clampfed so cant
        // y = -8x + 90
        var distance = Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        float projectileSpread;
        if (distance is >= 0 and <= 10)
        {
            projectileSpread = -8 * distance + 90;
        }
        else
        {
            projectileSpread = 30;
        }

        float angle = GetAngleToTarget() + projectileSpread / 2f;
        float angleIncrease;
        if (numberOfProjectiles != 1)
        {
            angleIncrease = projectileSpread / ((float)numberOfProjectiles - 1);
        }
        else
        {
            angleIncrease = 0;
        }

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float tempRot = angle - angleIncrease * i;
            MultiProjectileAbility projectile =
                Instantiate(this, transform.position, Quaternion.Euler(0, 0, tempRot + rotationOffset));
            projectile.SetAbilityStats(stats);
            projectile.SetTarget(target, targetFaction);
            projectile.SetLayer(targetFaction);
        }

        Destroy(gameObject); // Destroy the original projectile
    }
}