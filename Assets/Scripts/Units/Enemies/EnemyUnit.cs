using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyUnit : Unit
{
    [HideInInspector] public AIDestinationSetter destinationSetter;

    // A* controls the movement of the enemy, it also has stopping distance, speed, slowdown distance..
    [HideInInspector] public AIPath aiPath;

    private Action rotateToPlayer;

    protected override void Awake()
    {
        base.Awake();
        destinationSetter = gameObject.GetComponent<AIDestinationSetter>();
        aiPath = gameObject.GetComponent<AIPath>();
        destinationSetter.target = GameObject.FindWithTag("Player").transform;
    }

    protected override void Start()
    {
        // determine which way to flip enemy based on default spirte direction and position of player relative to enemy
        if (isFacingRight)
        {
            rotateToPlayer = () => sprite.flipX = destinationSetter.target.position.x < transform.position.x;
        }
        else
        {
            rotateToPlayer = () => sprite.flipX = destinationSetter.target.position.x > transform.position.x;
        }
    }

    protected override void Update()
    {
        base.Update();
        rotateToPlayer();
    }

    public override void SetAttributes(Attributes newAttributes)
    {
        base.SetAttributes(newAttributes);
        aiPath.maxSpeed = attributes.speed.initial;
    }

    public override void TakeDamage(float damage)
    {
        // flash red
        StartCoroutine(FlashRed());
        base.TakeDamage(damage);
    }

    private IEnumerator FlashRed()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}