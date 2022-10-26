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
public struct PlayerStats
{
    public int xp;
    public int level;
    public int gold;
}