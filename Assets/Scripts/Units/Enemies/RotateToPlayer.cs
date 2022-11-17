using System;
using UnityEngine;

public class RotateToPlayer : MonoBehaviour
{
    private EnemyUnit enemyUnit;
    private Action rotateToPlayer;

    private void Start()
    {
        enemyUnit = GetComponent<EnemyUnit>();
        if (enemyUnit.isFacingRight)
        {
            rotateToPlayer = () =>
                enemyUnit.sprite.flipX = enemyUnit.player.transform.position.x < transform.position.x;
        }
        else
        {
            rotateToPlayer = () =>
                enemyUnit.sprite.flipX = enemyUnit.player.transform.position.x > transform.position.x;
        }
    }

    private void Update()
    {
        if (enemyUnit.state != EnemyUnit.EnemyState.Dead)
        {
            rotateToPlayer();
        }
    }
}