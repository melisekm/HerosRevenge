using UnityEngine;

public class RangedEnemyUnit : EnemyUnit
{
    public Ability projectilePrefab;

    protected override void AttackPlayer()
    {
        ActivateAbility(transform.position, player.transform.position);
    }

    protected void ActivateAbility(Vector3 startPosition, Vector3 targetPosition)
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
            projectile.Activate(stats, targetPosition, Faction.Player);
        }
    }
}