using UnityEngine;
using Utils;

public class RangedEnemyUnit : EnemyUnit
{
    public Ability projectilePrefab;

    protected override void AttackPlayer()
    {
        ActivateAbility(transform.position);
    }

    protected void ActivateAbility(Vector3 startPosition)
    {
        Ability projectile = Instantiate(projectilePrefab, startPosition, Quaternion.identity);
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