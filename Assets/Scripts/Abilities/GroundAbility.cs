using Unity.VisualScripting;
using UnityEngine;

public class GroundAbility : Ability
{
    public float attackCooldown = 0.5f;
    private float attackTimer;
    public float dissapearTime = 2f;

    private void Start()
    {
        Destroy(gameObject, dissapearTime);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (attackTimer <= 0)
        {
            Act(collision);
            attackTimer = attackCooldown;
        }
    }

    protected void Update()
    {
        if (attackTimer >= 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }
}