using UnityEngine;

public class BoomingVoice : Ability
{
    private void Start()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var player = GameObject.FindWithTag("Player");
        float bonusDamage = 0;
        if (player && player.TryGetComponent(out PlayerUnit playerUnit))
        {
            bonusDamage = playerUnit.attributes.attackPower.actual;
        }
        foreach (var enemy in enemies)
        {
            var enemyUnit = enemy.GetComponent<EnemyUnit>();
            enemyUnit.TakeDamage(enemyUnit.attributes.health.initial * 0.5f + bonusDamage);
            hitEffect.Activate(enemyUnit.transform.position);
        }

        Destroy(gameObject);
    }
}