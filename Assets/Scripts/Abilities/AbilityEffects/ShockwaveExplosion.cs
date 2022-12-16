using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ShockwaveExplosion : MonoBehaviour
{
    private float radius = 1f;
    private Faction faction;
    private float damage;

    private void Start()
    {
        // get circle 2d collider
        radius = GetComponent<CircleCollider2D>().radius;
    }

    public void Initialize(float damage, Faction faction)
    {
        this.damage = damage;
        this.faction = faction;
    }

    // called by the animation event
    public void Explode()
    {
        // circle overlap to find all colliders in the explosion radius
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D collision in collisions)
        {
            if (collision.gameObject.TryGetComponent(out Unit unit))
            {
                if (unit.faction == faction)
                {
                    unit.TakeDamage(damage);
                }
            }
        }
    }

    // called by the animation event
    public void Disappear()
    {
        Destroy(gameObject);
    }
}