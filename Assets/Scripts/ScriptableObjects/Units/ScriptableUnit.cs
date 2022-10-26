using System;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class ScriptableUnit : ScriptableObject
{
    public Attributes attributes;
    public Unit prefab;
}

[Serializable]
public struct Attributes
{
    public int health;
    public int attackPower;
    public float defenseRating;
    public float speed;
    public float cooldownRecovery;
}

