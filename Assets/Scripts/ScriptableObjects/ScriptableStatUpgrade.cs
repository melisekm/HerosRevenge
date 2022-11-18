using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Upgrades/New Scriptable Stat Upgrade")]
public class ScriptableStatUpgrade : ScriptableReward
{
    public StatType statType;
    public int amount;
}

[Serializable]
public enum StatType
{
    Health,
    Damage,
    Speed,
    Defense,
    CooldownRecovery,
    PickupRange
}