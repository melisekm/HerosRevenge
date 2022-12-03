using System;
using System.Linq;
using Pathfinding;
using UnityEngine;

public class EnemyUnit : Unit
{
    public static event Action<EnemyUnit> OnEnemyUnitDied;
    public float attackCooldown = 1f;
    public float deathDelay;
    private float attackTimer;
    private RotateToPlayer rotateToPlayer;

    public Energy dropOnDeath;
    [NonSerialized] public int energyDropAmount = 10;

    protected GameObject player;

    // A* controls the movement of the enemy, it also has stopping distance, speed, slowdown distance..
    protected AIPath aiPath;
    protected AIDestinationSetter destinationSetter;

    private EnemyState state = EnemyState.Moving;

    private enum EnemyState
    {
        Moving,
        Attacking,
        Dead
    }

    private Animator animator;
    private static readonly int Dying = Animator.StringToHash("Dying");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Speed = Animator.StringToHash("Speed");

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        aiPath = GetComponent<AIPath>();
        rotateToPlayer = GetComponent<RotateToPlayer>();
        player = GameObject.FindWithTag("Player");
        if (player)
            destinationSetter.target = player.transform;
        else
            Debug.LogError("Player not found");

        faction = Faction.Enemy;
    }


    // overriden in child classes
    protected virtual void AttackPlayer()
    {
    }

    private void CheckDistance()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance > aiPath.endReachedDistance)
        {
            state = EnemyState.Moving;
        }
        else if (distance <= aiPath.endReachedDistance && attackTimer <= 0)
        {
            state = EnemyState.Attacking;
        }
    }

    public override void SetAttributes(Attributes newAttributes)
    {
        base.SetAttributes(newAttributes);
        aiPath.maxSpeed = attributes.speed.initial;
    }

    protected override void Die()
    {
        if (state == EnemyState.Dead) return;
        state = EnemyState.Dead;


        if (aiPath)
        {
            aiPath.canMove = false;
            destinationSetter.enabled = false;
        }

        if (rotateToPlayer)
        {
            rotateToPlayer.enabled = false;
        }

        // set gameobject layer to background so it doesn't collide with anything
        gameObject.layer = LayerMask.NameToLayer("BackgroundLayer");

        if (dropOnDeath)
        {
            Energy energy = Instantiate(dropOnDeath, transform.position, Quaternion.identity);
            energy.amount = energyDropAmount;
        }

        if (animator && animator.parameters.Any(p => p.type == AnimatorControllerParameterType.Trigger))
        {
            // Dying animation after animation has OnAnimationStart which calls Destroy after animation is done
            animator.SetTrigger(Dying);
        }

        base.DieAfterDelay(deathDelay);
        OnEnemyUnitDied?.Invoke(this);
    }

    protected virtual void Update()
    {
        if (!player) return;

        animator.SetFloat(Speed, Mathf.Abs(aiPath.desiredVelocity.x));

        if (attackTimer >= 0)
        {
            attackTimer -= Time.deltaTime;
        }

        if (state != EnemyState.Dead)
        {
            CheckDistance();
        }

        if (state == EnemyState.Attacking)
        {
            if (attackTimer <= 0)
            {
                animator.SetTrigger(Attack);
                AttackPlayer();
                attackTimer = attackCooldown;
            }
        }
    }
}