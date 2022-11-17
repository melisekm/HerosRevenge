using System;
using UnityEngine;

public class GroundAbility : Ability
{
    public float attackCooldown = 0.5f;
    private float attackTimer;
    public float dissapearTime = 2f;
    private bool isActive = true;

    private void Start()
    {
        attackTimer = attackCooldown;
    }

    protected override void Act(Collider2D collision)
    {
        if (isActive && attackTimer <= 0)
        {
            base.Act(collision);
            attackTimer = attackCooldown;
        }
    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Act(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Act(collision);
    }

    protected void Update()
    {
        if (!isActive) return;

        if (attackTimer >= 0)
        {
            attackTimer -= Time.deltaTime;
        }
        
        dissapearTime -= Time.deltaTime;
        if (dissapearTime <= 0)
        {
            isActive = false;
            Die();
        }
    }
}