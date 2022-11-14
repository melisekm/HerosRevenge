using System;
using Pathfinding;
using UnityEngine;
using Utils;

public class EnemyUnit : Unit
{
    public float attackCooldown = 1f;
    private float attackTimer;
    protected GameObject player;
    [HideInInspector] public AIDestinationSetter destinationSetter;

    // A* controls the movement of the enemy, it also has stopping distance, speed, slowdown distance..
    [HideInInspector] public AIPath aiPath;

    private Action rotateToPlayer;

    private enum State
    {
        Moving,
        Attacking,
    }

    private State state = State.Moving;

    public GameObject dropOnDeath;

    protected override void Awake()
    {
        base.Awake();
        destinationSetter = gameObject.GetComponent<AIDestinationSetter>();
        aiPath = gameObject.GetComponent<AIPath>();
        player = GameObject.FindGameObjectWithTag("Player");
        destinationSetter.target = player.transform;
        faction = Faction.Enemy;
    }

    protected void Start()
    {
        // determine which way to flip enemy based on default spirte direction and position of player relative to enemy
        if (isFacingRight)
        {
            rotateToPlayer = () => sprite.flipX = player.transform.position.x < transform.position.x;
        }
        else
        {
            rotateToPlayer = () => sprite.flipX = player.transform.position.x > transform.position.x;
        }
    }

    protected void Update()
    {
        if (!player) return;

        if (attackTimer >= 0)
        {
            attackTimer -= Time.deltaTime;
        }

        rotateToPlayer();
        CheckDistance();
        StateDecision();
    }

    private void StateDecision()
    {
        switch (state)
        {
            case State.Moving:
            {
                // FollowTarget();
                break;
            }
            case State.Attacking:
                if (attackTimer <= 0)
                {
                    AttackPlayer();
                    attackTimer = attackCooldown;
                }

                break;
        }
    }

    private void CheckDistance()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance > aiPath.endReachedDistance)
        {
            // aiPath.canMove = true;
            state = State.Moving;
        }
        else if (distance <= aiPath.endReachedDistance && attackTimer <= 0)
        {
            // aiPath.canMove = false;
            state = State.Attacking;
        }
    }

    protected virtual void FollowTarget()
    {
    }


    protected virtual void AttackPlayer()
    {
        
    }

    public override void SetAttributes(Attributes newAttributes)
    {
        base.SetAttributes(newAttributes);
        aiPath.maxSpeed = attributes.speed.initial;
    }

    protected override void Die()
    {
        // TODO: add death animation
        // TODO: add death sound?
        if (dropOnDeath)
        {
            Instantiate(dropOnDeath, transform.position, Quaternion.identity);
        }
        base.Die();
    }
}