using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class GroundAbility : Ability
{
    public float attackCooldown = 0.5f;
    private float attackTimer;
    public float dissapearTime = 2f;
    private bool isActive = true;
    private float radius;

    private void Start()
    {
        radius = GetComponent<CircleCollider2D>().radius;
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
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (Collider2D collider in colliders)
            {
                Act(collider);
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