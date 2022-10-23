using System;
using UnityEngine;

public class ScriptableUnit : ScriptableObject
{
    public Stats stats;
    public Unit prefab;
}

[Serializable]
public struct Stats
{
    public int health;
    public int attackPower;
    public float defenseRating;
    public float speed;
    public float cooldownRecovery;
}

