using UnityEngine;

public class LightningBlast : MultiProjectileAbility
{
    public LightningEffect lightningEffect;
    public float damageMultiplier = 0.1f;
    public float damageRadius = 2.0f;
    private Collider2D[] collisionResults = new Collider2D[10];


    protected override void Die()
    {
        int size = Physics2D.OverlapCircleNonAlloc(transform.position, damageRadius, collisionResults);
        for (var index = 0; index < size; index++)
        {
            var collision = collisionResults[index];
            if (collision.gameObject.TryGetComponent(out Unit unit))
            {
                if (targetFaction == unit.faction)
                {
                    var bolt = Instantiate(lightningEffect, transform.position,
                        Quaternion.identity);
                    bolt.Initialize(transform.position, unit.transform.position);
                    unit.TakeDamage(abilityStats.damage * damageMultiplier);
                }
            }
        }

        base.Die();
    }
}