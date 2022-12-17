using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class GroundAbility : Ability
{
    public float attackCooldown = 0.5f;
    public float dissapearTime = 2f;
    private float attackTimer;
    private Collider2D[] collisionResults = new Collider2D[20];
    protected bool isActive = true;
    private float radius;
    private int targetLayerMask;

    protected virtual void Start()
    {
        radius = GetComponent<CircleCollider2D>().radius;
    }

    protected virtual void Update()
    {
        if (!isActive) return;
        if (attackTimer < attackCooldown)
        {
            attackTimer += Time.deltaTime;
        }
        // periodically check for physics.overlapcircle to hit enemies
        else
        {
            int size = Physics2D.OverlapCircleNonAlloc(transform.position, radius, collisionResults, targetLayerMask);
            for (var index = 0; index < size; index++)
            {
                Act(collisionResults[index]);
            }

            attackTimer = 0;
        }

        dissapearTime -= Time.deltaTime;
        if (dissapearTime <= 0)
        {
            isActive = false;
            Die();
        }
    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Act(collision);
    }

    public override void Activate(AbilityStats stats, Vector3 target, Faction targetFaction)
    {
        base.Activate(stats, target, targetFaction);
        transform.position = new Vector3(target.x, target.y, 0);
    }

    protected override void Act(Collider2D collision)
    {
        if (isActive)
        {
            base.Act(collision);
        }
    }

    protected override void SetLayer(Faction targetFaction)
    {
        if (targetFaction == Faction.Player)
        {
            gameObject.layer = LayerMask.NameToLayer("GroundAbilityLayer");
            targetLayerMask = LayerMask.GetMask("Default");
        }
        else
        {
            targetLayerMask = LayerMask.GetMask("EnemyLayer");
        }
    }
}