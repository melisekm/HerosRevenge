using System;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class ScriptableUnit : ScriptableObject
{
    public Attributes attributes;
    public Unit prefab;
}

[Serializable]
public class Attributes
{
    public Attributes(Attributes other)
    {
        health = new Attribute(other.health);
        attackPower = new Attribute(other.attackPower);
        speed = new Attribute(other.speed);
        defenseRating = new Attribute(other.defenseRating);
        cooldownRecovery = new Attribute(other.cooldownRecovery);
    }

    [SerializeField] public Attribute health;
    [SerializeField] public Attribute attackPower;
    [SerializeField] public Attribute defenseRating;
    [SerializeField] public Attribute speed;
    [SerializeField] public Attribute cooldownRecovery;
}

[Serializable]
public class Attribute
{
    public Attribute(Attribute other)
    {
        initial = other.initial;
        actual = other.actual;
        min = other.min;
        max = other.max;
        increasePerLevel = other.increasePerLevel;
    }
    public Attribute(float initial, float actual, float min, float max, float increasePerLevel)
    {
        this.initial = initial;
        this.actual = actual;
        this.min = min;
        this.max = max;
        this.increasePerLevel = increasePerLevel;
    }

    [SerializeField] public float initial;
    [SerializeField] public float actual;
    [SerializeField] public float min;
    [SerializeField] public float max;
    [SerializeField] public float increasePerLevel;
}