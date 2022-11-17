using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

public class RangedAbility : Ability
{
    public float speed = 10f;
    private float flownDistance;

    private bool canMove = true;
    protected int rotationOffset = -90;


    
    public override void Activate(AbilityStats stats, Vector3 target, Faction targetFaction)
    {
        base.Activate(stats, target, targetFaction);
        float angle = GetAngleToTarget();
        transform.rotation = Quaternion.Euler(0f, 0f, angle + rotationOffset);
    }
    

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