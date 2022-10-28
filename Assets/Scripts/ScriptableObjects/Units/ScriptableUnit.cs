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
    [SerializeField] public Attribute health;
    [SerializeField] public Attribute attackPower;
    [SerializeField] public Attribute defenseRating;
    [SerializeField] public Attribute speed;
    [SerializeField] public Attribute cooldownRecovery;
}

[Serializable]
public class Attribute
{
    public Attribute(int initial, int actual, int min, int max, int increasePerLevel)
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