using System;
using UnityEngine;

public class ScriptablePowerUp : ScriptableObject
{
    public PowerUpType powerUpType;
    public GameObject prefab;
}

[Serializable]
public enum PowerUpType
{
    Treasure,
    Ability,
    Stat
}