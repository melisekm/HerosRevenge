using System.Collections;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    protected SpriteRenderer sprite;
    public Faction faction { get; protected set; }
    public Attributes attributes { get; private set; }

    protected virtual void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public virtual void SetAttributes(Attributes attr) => attributes = attr;

    public virtual void TakeDamage(float damage)
    {
        if (attributes.health.actual == attributes.health.min) return;

        StartCoroutine(Flash());
        int damageTaken = Mathf.RoundToInt(damage * (1 - attributes.defenseRating.actual));
        attributes.health.actual -= damageTaken;

        if (attributes.health.actual <= attributes.health.min)
        {
            attributes.health.actual = attributes.health.min;
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected virtual void DieAfterDelay(float delay = 0.5f)
    {
        Destroy(gameObject, delay);
    }

    private IEnumerator Flash(Color color = default)
    {
        if (color == default)
        {
            color = Color.red;
        }

        sprite.color = color;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;
    }
}

public enum Faction
{
    Player,
    Enemy
}