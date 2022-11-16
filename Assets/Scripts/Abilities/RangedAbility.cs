using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RangedAbility : Ability
{
    public float speed = 10f;
    private float flownDistance;

    private bool canMove = true;
    private static readonly int PlayFinished = Animator.StringToHash("playFinished");

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

    protected override void Die()
    {
        canMove = false;
        // either has a custom animation or set specific particle hit effect and just dissapears
        
        if (animator && animator.parameters.Any(p => p.type == AnimatorControllerParameterType.Trigger))
        {
            // if trigger parameter exists in animator
            animator.SetTrigger(PlayFinished);
        }
        else
        {
            if (hitEffect)
            {
                hitEffect.Activate();
            }

            Destroy(gameObject);
        }
    }
}