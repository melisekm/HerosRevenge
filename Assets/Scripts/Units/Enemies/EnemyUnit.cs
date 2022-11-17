using System.Linq;
using Pathfinding;
using UnityEngine;
using Utils;

public class EnemyUnit : Unit
{
    public float attackCooldown = 1f;
    private float attackTimer;
    public float deathDelay;

    public Energy dropOnDeath;

    [HideInInspector] public GameObject player;

    [HideInInspector] public AIDestinationSetter destinationSetter;

    // A* controls the movement of the enemy, it also has stopping distance, speed, slowdown distance..
    [HideInInspector] public AIPath aiPath;
    [HideInInspector] public int energyDropAmount = 10;


    [HideInInspector] public EnemyState state = EnemyState.Moving;

    public enum EnemyState
    {
        Moving,
        Attacking,
        Dead
    }

    protected Animator animator;
    private static readonly int Dying = Animator.StringToHash("Dying");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Speed = Animator.StringToHash("Speed");

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
        state = EnemyState.Dead;
        // disable path finding
        aiPath.canMove = false;
        destinationSetter.enabled = false;
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
    }
}