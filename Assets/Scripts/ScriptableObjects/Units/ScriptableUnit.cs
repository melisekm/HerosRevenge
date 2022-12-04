using System;
using UnityEngine;

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
        pickupRange = new Attribute(other.pickupRange);
    }

    [SerializeField] public Attribute health;
    [SerializeField] public Attribute attackPower;
    [SerializeField] public Attribute defenseRating;
    [SerializeField] public Attribute speed;
    [SerializeField] public Attribute cooldownRecovery;
    [SerializeField] public Attribute pickupRange;
}

[Serializable]
public class Attribute
{
    [SerializeField] public float initial;

    [SerializeField] private float _actual;

    public float actual
    {
        get => _actual;
        set
        {
            _actual = value;
            OnValueChanged?.Invoke(this);
        }
    }

    [SerializeField] public float min;
    [SerializeField] public float max;
    [SerializeField] public float increasePerLevel;
    public event Action<Attribute> OnValueChanged;

    public void ToggleUpgrade(float value, int modifier = 1)
    {        
        // if he was on max attrbute, lost boost do not lower it

        if (modifier == -1 && Math.Abs(initial - max) < 0.001) return;
        initial = Mathf.Clamp(initial + value * modifier, min + 0.01f, max);
        actual = Mathf.Clamp(actual + value * modifier, min + 0.01f, max);
    }

    public void LevelUp()
    {
        initial = Mathf.Min(initial + increasePerLevel, max);
        actual = initial;
    }

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
}