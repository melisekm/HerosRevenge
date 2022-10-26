using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableAbility : ScriptableObject
{
  
    public Sprite sprite;
    public AbilityStats stats;
    

    public abstract void Initialize(GameObject obj);
    public abstract void Trigger();
}
[Serializable]
public struct AbilityStats
{
    public int damage;
    public float baseCooldown;
    public float range;
}

[Serializable]
public enum AbilityType
{
    
}