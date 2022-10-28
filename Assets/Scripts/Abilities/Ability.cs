using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    protected AbilityStats abilityStats { get; private set; }

    public virtual void SetAbilityStats(AbilityStats st) => abilityStats = st;
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}
