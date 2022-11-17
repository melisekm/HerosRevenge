using System;
using UnityEngine;
using Utils;

public class GroundAbility : Ability
{
    public float attackCooldown = 0.5f;
    private float attackTimer;
    public float dissapearTime = 2f;
    private bool isActive = true;

    public override void Activate(AbilityStats stats, Vector3 target, Faction targetFaction)
    {
        base.Activate(stats, target, targetFaction);
        transform.position = new Vector3(target.x, target.y, 0);
    }
    
    protected override void Act(Collider2D collision)
    {
        if (isActive && attackTimer <= 0)
        {
            base.Act(collision);
            attackTimer = attackCooldown;
        }
    }
    
    protected override void SetLayer(Faction targetFaction)
    {
        if (targetFaction == Faction.Player)
        {
            gameObject.layer = LayerMask.NameToLayer("GroundAbilityLayer");
        }
    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Act(collision);
    }

    protected void OnTriggerStay2D(Collider2D collision)
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