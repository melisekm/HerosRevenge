using UnityEngine;

public class ArchimedesUnit : RangedEnemyUnit
{
    public float randomRadius = 1f;
    protected override void AttackPlayer()
    {
        // get random position sphere around player
        var randomPosition = player.transform.position + Random.insideUnitSphere * randomRadius;
        ActivateAbility(randomPosition);
    }
}
