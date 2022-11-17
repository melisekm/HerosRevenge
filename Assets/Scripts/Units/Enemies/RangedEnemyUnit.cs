using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class RangedEnemyUnit : EnemyUnit
{
    public Ability projectilePrefab;

    protected override void AttackPlayer()
    {
        Ability projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        if (projectile)
        {
            // TODO: remove hardcoded number if attributes was not set (Spawned from Editor)
            float projectileDamage = attributes == null ? 10f : attributes.attackPower.actual; 

            AbilityStats stats = new AbilityStats
            {
                damage = projectileDamage,
                range = aiPath.endReachedDistance,
            };
            projectile.Activate(stats, player.transform.position, Faction.Player);
        }
    }
}