using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Abilities/New Scriptable Ability")]
public class ScriptableAbility : ScriptableReward
{
    public AbilityStats stats;
    public AbilityType abilityType;
    public Ability prefab;
    public float minLevel;
    public AbilityGroup group;
}

[Serializable]
public struct AbilityStats
{
    public float damage;
    public float baseCooldown;
    public float range;
}

[Serializable]
public enum AbilityGroup
{
    Regular,
    Ultimate
}

[Serializable]
public enum AbilityType
{
    Empty,
    Cleave,
    FireOrb,
    PiercingShot,
    PoisonRain,
    BoomingVoice,
}