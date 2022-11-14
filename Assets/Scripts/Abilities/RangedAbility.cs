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
        if (animator && animator.parameters.Any(p => p.type == AnimatorControllerParameterType.Trigger))
        {
            // if trigger parameter exists in animator
            animator.SetTrigger(PlayFinished);
            canMove = false;
            Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
        }
        else
        {
            if (hitEffect)
            {
                var hitEffectMain = hitEffect.main;
                hitEffectMain.startColor = new ParticleSystem.MinMaxGradient(hitColor);
                Instantiate(hitEffect, transform.position + transform.up, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}