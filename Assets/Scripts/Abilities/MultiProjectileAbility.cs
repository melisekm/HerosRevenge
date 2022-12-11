using UnityEngine;

public class MultiProjectileAbility : RangedAbility
{
    public int numberOfProjectiles = 1;

    public override void Activate(AbilityStats stats, Vector3 target, Faction targetFaction)
    {
        base.Activate(stats, target, targetFaction);
        // apply linear regression to calculate the spread of the projectiles based on distance from mouse  y = -8x + 90
        // target is clampfed so cant
        var distance = Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        float projectileSpread;
        if (numberOfProjectiles > 1 && distance is >= 0 and <= 10)
            projectileSpread = -8 * distance + 90;
        else
            projectileSpread = 0;

        // https://www.youtube.com/watch?v=uXLomV2TAMw
        float angle = GetAngleToTarget() + projectileSpread / 2f;
        var angleIncrease = numberOfProjectiles != 1 ? projectileSpread / ((float)numberOfProjectiles - 1) : 0;

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