using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ShockwaveExplosion : MonoBehaviour
{
    private Collider2D[] collisionResults = new Collider2D[5];
    private float damage;
    private Faction faction;
    private float radius = 1f;

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
        var size = Physics2D.OverlapCircleNonAlloc(transform.position, radius, collisionResults);
        foreach (Collider2D collision in collisionResults)
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