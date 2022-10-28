using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Units/New Scriptable Player")]
public class ScriptablePlayer : ScriptableUnit
{
    public PlayerStats playerStats;
}

[Serializable]
public class PlayerStats
{
    public float xp;
    [SerializeField] public Attribute level;
    [SerializeField] public Attribute gold;
}