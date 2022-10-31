using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public AbilityStats abilityStats { get; private set; }

    public virtual void SetAbilityStats(AbilityStats st) => abilityStats = st;
}