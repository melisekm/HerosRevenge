using System.Linq;

public class BoomingVoice : Ability
{
    private void Start()
    {
        var enemies = GameManager.Instance.enemies;
        foreach (var enemy in enemies.ToList())
        {
            var enemyUnit = enemy.Value;
            enemyUnit.TakeDamage(enemyUnit.attributes.health.initial * 0.5f);
            hitEffect.Activate(enemyUnit.transform.position);
        }
        Destroy(gameObject);
    }

}
