using System.Linq;
using UnityEngine;

public class BoomingVoice : Ability
{
    private void Start()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            var enemyUnit = enemy.GetComponent<EnemyUnit>();
            enemyUnit.TakeDamage(enemyUnit.attributes.health.initial * 0.5f);
            hitEffect.Activate(enemyUnit.transform.position);
        }
        Destroy(gameObject);
    }

}
