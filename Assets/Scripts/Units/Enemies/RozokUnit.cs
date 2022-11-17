using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RozokUnit : EnemyUnit
{

    protected override void AttackPlayer()
    {
        if (player && player.TryGetComponent(out PlayerUnit playerUnit))
        {
            animator.SetTrigger("Attack");

            playerUnit.TakeDamage(attributes.attackPower.actual);
        }
    }

    protected override void Update()
    {
        base.Update();
        animator.SetFloat("Speed", Mathf.Abs(aiPath.desiredVelocity.x));
        
    }
}
