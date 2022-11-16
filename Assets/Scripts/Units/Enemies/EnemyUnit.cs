using System;
using System.Linq;
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
        Dead
    }

    private State state = State.Moving;

    public GameObject dropOnDeath;
    
    private Animator animator;
    private static readonly int Dying = Animator.StringToHash("Dying");

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        destinationSetter = gameObject.GetComponent<AIDestinationSetter>();
        aiPath = gameObject.GetComponent<AIPath>();
        player = GameObject.FindGameObjectWithTag("Player");
        destinationSetter.target = player.transform;
        faction = Faction.Enemy;
    }

    protected virtual void Start()
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

        if (state != State.Dead)
        {
            rotateToPlayer();
            CheckDistance();
        }

        if (state == State.Attacking)
        {
            if (attackTimer <= 0)
            {
                AttackPlayer();
                attackTimer = attackCooldown;
            }
        }
    }


    private void CheckDistance()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance > aiPath.endReachedDistance)
        {
            state = State.Moving;
        }
        else if (distance <= aiPath.endReachedDistance && attackTimer <= 0)
        {
            state = State.Attacking;
        }
    }


    protected virtual void AttackPlayer()
    {
        // overriden in child classes TODO: make abstract
    }

    public override void SetAttributes(Attributes newAttributes)
    {
        base.SetAttributes(newAttributes);
        aiPath.maxSpeed = attributes.speed.initial;
    }

    protected override void Die()
    {
        state = State.Dead;
        aiPath.canMove = false;
        // set gameobject layer to background so it doesn't collide with anything
        gameObject.layer = LayerMask.NameToLayer("BackgroundLayer");

        if (dropOnDeath)
        {
            Instantiate(dropOnDeath, transform.position, Quaternion.identity);
        }

        if (animator && animator.parameters.Any(p => p.type == AnimatorControllerParameterType.Trigger))
        {
            // Dying animation after animation has OnAnimationStart which calls Destroy after animation is done
            animator.SetTrigger(Dying);
        }
        else
        {
            base.Die();
        }
    }
}