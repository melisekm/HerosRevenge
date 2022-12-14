using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Upgrades/New Scriptable Stat Upgrade")]
public class ScriptableStatUpgrade : ScriptableReward
{
    public StatType statType;
    public float amount;
}

[Serializable]
public enum StatType
{
    Health,
    Damage,
    Speed,
    Defense,
    CooldownRecovery,
    PickupRange,
    Gold
}