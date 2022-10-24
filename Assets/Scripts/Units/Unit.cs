using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sprite;
    public Stats Stats { get; private set; }

    public virtual void SetStats(Stats stats) => Stats = stats;

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

    public virtual void TakeDamage(int damage)
    {
    }
}