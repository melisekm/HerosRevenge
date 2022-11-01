using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleEnemyUnit : EnemyUnit
{

    protected override void AttackPlayer()
    {
        if (player && player.TryGetComponent(out PlayerUnit playerUnit))
        {
            playerUnit.TakeDamage(attributes.attackPower.actual);
        }
    }
}
