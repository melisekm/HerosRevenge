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
    [SerializeField] public Attribute health;
    [SerializeField] public Attribute attackPower;
    [SerializeField] public Attribute defenseRating;
    [SerializeField] public Attribute speed;
    [SerializeField] public Attribute cooldownRecovery;
    [SerializeField] public Attribute pickupRange;

    public Attributes(Attributes other)
    {
        health = new Attribute(other.health);
        attackPower = new Attribute(other.attackPower);
        speed = new Attribute(other.speed);
        defenseRating = new Attribute(other.defenseRating);
        cooldownRecovery = new Attribute(other.cooldownRecovery);
        pickupRange = new Attribute(other.pickupRange);
    }
}

[Serializable]
public class Attribute
{
    [SerializeField] public float initial;

    [SerializeField] private float _actual;

    [SerializeField] public float min;
    [SerializeField] public float max;
    [SerializeField] public float increasePerLevel;
    [SerializeField] public int upgradeCost;

    public Attribute(Attribute other)
    {
        initial = other.initial;
        actual = other.actual;
        min = other.min;
        max = other.max;
        increasePerLevel = other.increasePerLevel;
        upgradeCost = other.upgradeCost;
    }

    public Attribute(float initial, float actual, float min, float max, float increasePerLevel, int upgradeCost)
    {
        this.initial = initial;
        this.actual = actual;
        this.min = min;
        this.max = max;
        this.increasePerLevel = increasePerLevel;
        this.upgradeCost = upgradeCost;
    }

    public float actual
    {
        get => _actual;
        set
        {
            _actual = value;
            OnValueChanged?.Invoke(this);
        }
    }

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

    public void BuyUpgrade(float multiplier)
    {
        initial = Mathf.Min(initial + increasePerLevel, max);
        actual = initial;
        upgradeCost = Mathf.RoundToInt(upgradeCost * multiplier);
    }
}