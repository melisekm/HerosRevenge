using UnityEngine;

public class BoomingVoice : Ability, IUltimateEventInvokable
{
    private IUltimateEventInvokable ultimateEventInvokable;
    public float damagePercentage = 0.5f;
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
            enemyUnit.TakeDamage(enemyUnit.attributes.health.initial * damagePercentage + bonusDamage);
            hitEffect.Activate(enemyUnit.transform.position);
        }

        ultimateEventInvokable = this;
        ultimateEventInvokable.InvokeUltimateEvent(IUltimateEventInvokable.EndOfActivation);
        Destroy(gameObject);
    }
}