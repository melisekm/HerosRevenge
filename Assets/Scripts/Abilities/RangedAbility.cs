using UnityEngine;

public class RangedAbility : Ability
{
    public float speed = 10f;
    protected bool canMove = true;
    private float flownDistance;
    protected readonly int rotationOffset = -90;

    protected virtual void Update()
    {
        if (canMove)
        {
            transform.position += transform.up * (speed * Time.deltaTime);
            flownDistance += speed * Time.deltaTime;
            if (flownDistance >= abilityStats.range)
            {
                Die();
            }
        }
    }

    public override void Activate(AbilityStats stats, Vector3 target, Faction targetFaction)
    {
        base.Activate(stats, target, targetFaction);
        float angle = GetAngleToTarget();
        transform.rotation = Quaternion.Euler(0f, 0f, angle + rotationOffset);
    }

    protected float GetAngleToTarget()
    {
        // https://answers.unity.com/questions/995540/move-towards-mouse-direction-infinitely-at-constan.html
        Vector3 targetPos = new Vector3(target.x, target.y, 0);
        Vector3 diff = targetPos - transform.position;
        diff.Normalize();
        return Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
    }

    protected override void Die()
    {
        canMove = false;
        base.Die();
    }
}