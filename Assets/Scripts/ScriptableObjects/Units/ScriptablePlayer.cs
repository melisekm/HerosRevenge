using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Units/New Scriptable Player")]
public class ScriptablePlayer : ScriptableUnit
{
    public PlayerStats playerStats;
}


[Serializable]
public class PlayerStats
{
    [SerializeField] public Attribute xp;
    [SerializeField] public Attribute level;
    [SerializeField] public Attribute gold;

    public PlayerStats(PlayerStats other)
    {
        xp = new Attribute(other.xp);
        level = new Attribute(other.level);
        gold = new Attribute(other.gold);
    }

    public PlayerStats(Attribute xp, Attribute level, Attribute gold)
    {
        this.xp = new Attribute(xp);
        this.level = new Attribute(level);
        this.gold = new Attribute(gold);
    }
}