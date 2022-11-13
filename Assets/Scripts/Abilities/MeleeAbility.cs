using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAbility : RangedAbility
{
    // private Animator animator;
    // private GameObject visual;
    
    protected override void Start()
    {
        base.Start();
        // visual = transform.GetChild(0).gameObject;
        // animator = visual.GetComponent<Animator>();
    }
    
    // protected void Update()
    // {
    //     // destroy gameobject after animation is done
    //     if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
    //     {
    //         Destroy(gameObject);
    //     }
    // }
}
