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

    // private void RotateToPlayer()
    // {
    //
    //     // sprite.flipX = isDefaultFacingRight
    //     //     ? destinationSetter.target.position.x < transform.position.x
    //     //     : destinationSetter.target.position.x > transform.position.x;
    //     bool facingLeftAndGoingRight = aiPath.desiredVelocity.x >= 0.01f && !isDefaultFacingRight;
    //     bool facingRightAndGoingLeft = aiPath.desiredVelocity.x <= -0.01f && isDefaultFacingRight;
    //     if (facingLeftAndGoingRight || facingRightAndGoingLeft)
    //     {
    //         sprite.flipX = true;
    //     }
    //     else
    //     {
    //         sprite.flipX = false;
    //     }
    // }

    public override void SetAttributes(Attributes newAttributes)
    {
        base.SetAttributes(newAttributes);
        aiPath.maxSpeed = attributes.speed;
    }
}