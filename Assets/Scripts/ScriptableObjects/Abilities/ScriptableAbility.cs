using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Abilities/New Scriptable Ability")]
public class ScriptableAbility : ScriptableReward
{
    public AbilityStats stats;
    public AbilityType abilityType;
    public Ability prefab;
    public float minLevel;
}

[Serializable]
public struct AbilityStats
{
    public float damage;
    public float baseCooldown;
    public float range;
}

[Serializable]
public enum AbilityType
{
    Empty = 0,
    Cleave = 1,
    FireOrb = 2,
    PiercingShot = 3,
    PoisonRain = 4,
}