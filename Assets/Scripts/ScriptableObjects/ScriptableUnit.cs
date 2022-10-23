using System;
using UnityEngine;

public class ScriptableUnit : ScriptableObject
{
    public Stats Stats;
    public Unit Prefab;
}

[Serializable]
public struct Stats
{
    public int Health;
    public int AttackPower;
    public float DefenseRating;
    public float Speed;
    public float CooldownRecovery;
}

