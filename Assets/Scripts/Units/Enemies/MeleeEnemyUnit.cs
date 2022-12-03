public class MeleeEnemyUnit : EnemyUnit
{
    protected override void AttackPlayer()
    {
        if (player && player.TryGetComponent(out PlayerUnit playerUnit))
        {
            playerUnit.TakeDamage(attributes.attackPower.actual);
        }
    }
}