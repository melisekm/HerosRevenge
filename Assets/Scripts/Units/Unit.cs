using System;
using System.Collections;
using UnityEngine;
using Utils;

public abstract class Unit : MonoBehaviour
{
    [NonSerialized] public Rigidbody2D rb;
    [NonSerialized] public SpriteRenderer sprite;
    public bool isFacingRight = true;

    // public get private set
    public Faction faction { get; protected set; }

    public Attributes attributes { get; private set; }

    public virtual void SetAttributes(Attributes attr) => attributes = attr;


    protected virtual void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }


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

    protected virtual void DieAfterDelay(float delay=0.5f)
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