using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class RangedEnemyUnit : EnemyUnit
{
    public Ability projectilePrefab;

    protected override void AttackPlayer()
    {
        FireProjectile();
    }

    private void FireProjectile()
    {
        Ability projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        if (projectile)
        {
            float projectileDamage;
            projectileDamage = attributes == null ? 10f : attributes.attackPower.actual;

            AbilityStats stats = new AbilityStats
            {
                damage = projectileDamage,
                range = aiPath.endReachedDistance,
            };
            projectile.SetAbilityStats(stats);
            projectile.SetTarget(player.transform.position, Faction.Player);
            projectile.enabled = true;
        }
    }
}