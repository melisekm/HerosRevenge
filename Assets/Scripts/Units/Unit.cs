using System.Collections;
using UnityEngine;
using Utils;

public abstract class Unit : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer sprite;
    [SerializeField] protected bool isFacingRight = true;

    // public get private set
    public Faction faction { get; protected set; }

    public Attributes attributes { get; private set; }

    public virtual void SetAttributes(Attributes attr) => attributes = attr;


    protected virtual void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        GameManager.OnBeforeStateChanged += OnStateChanged;
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