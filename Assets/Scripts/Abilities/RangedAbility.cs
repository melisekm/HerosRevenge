using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAbility : Ability
{
    public float speed = 10f;
    private float flownDistance;

    protected virtual void Update()
    {
        transform.position += transform.up * (speed * Time.deltaTime);

        flownDistance += speed * Time.deltaTime;
        if (flownDistance >= abilityStats.range)
        {
            Destroy(gameObject);
        }
    }

    
}