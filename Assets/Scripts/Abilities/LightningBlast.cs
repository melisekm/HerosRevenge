using UnityEngine;

public class LightningBlast : MultiProjectileAbility
{
    public LightningEffect lightningEffect;
    public float damageMultiplier = 0.1f;
    public float damageRadius = 2.0f;

    protected override void Die()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, damageRadius);
        foreach (Collider2D collision in collisions)
        {
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