using UnityEngine;
using Utils;

public abstract class Ability : MonoBehaviour
{
    public AbilityStats abilityStats { get; private set; }

    protected Vector3 target;
    protected Faction targetFaction;

    public void SetTarget(Vector3 target, Faction targetFaction)
    {
        this.target = target;
        this.targetFaction = targetFaction;
    }

    public virtual void SetAbilityStats(AbilityStats st) => abilityStats = st;
}