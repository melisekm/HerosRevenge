using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class GroundAbility : Ability
{
    public float attackCooldown = 0.5f;
    private float attackTimer;
    public float dissapearTime = 2f;
    private bool isActive = true;
    private float radius;
    private Collider2D[] collisionResults = new Collider2D[20];

    private void Start()
    {
        radius = GetComponent<CircleCollider2D>().radius;
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
        }
    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Act(collision);
    }

    protected void Update()
    {
        if (!isActive) return;
        if (attackTimer < attackCooldown)
        {
            attackTimer += Time.deltaTime;
        }
        // periodically check for physics.overlapcircle to hit enemies
        else
        {
            Physics2D.OverlapCircleNonAlloc(transform.position, radius, collisionResults);
            foreach (Collider2D collision in collisionResults)
            {
                Act(collision);
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
}