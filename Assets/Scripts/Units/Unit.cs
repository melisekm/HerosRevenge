using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer sprite;
    [SerializeField] protected bool isFacingRight = true;
    
    
    public Attributes attributes { get; private set; }

    public virtual void SetAttributes(Attributes attr) => attributes = attr;


    protected virtual void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        GameManager.OnBeforeStateChanged += OnStateChanged;
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
    }

    protected virtual void OnDestroy()
    {
        GameManager.OnBeforeStateChanged -= OnStateChanged;
    }

    protected virtual void OnStateChanged(GameState state)
    {
    }

    public virtual void TakeDamage(float damage)
    {
        int damageTaken = Mathf.RoundToInt(damage * (1 - attributes.defenseRating.actual));
        attributes.health.actual -= damageTaken;
        
        if (attributes.health.actual <= 0)
        {
            Die();
        }
        
        Debug.Log(gameObject.name + " took " + damage + " damage. Health: " + attributes.health.actual);
    }
    
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}