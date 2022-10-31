using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Units/New Scriptable Player")]
public class ScriptablePlayer : ScriptableUnit
{
    public PlayerStats playerStats;
}

[Serializable]
public class PlayerStats
{
    public PlayerStats(PlayerStats other)
    {
        xp = other.xp;
        level = new Attribute(other.level);
        gold = new Attribute(other.gold);
    }

    public PlayerStats(float xp, Attribute level, Attribute gold)
    {
        this.xp = xp;
        this.level = new Attribute(level);
        this.gold = new Attribute(gold);
    }

    public float xp;
    [SerializeField] public Attribute level;
    [SerializeField] public Attribute gold;
}