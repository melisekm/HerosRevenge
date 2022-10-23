using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scriptable Enemy")]
public class ScriptableEnemy : ScriptableUnit
{
    public EnemyType enemyType;
}

[Serializable]
public enum EnemyType
{
    Cpavok,
    Ded,
    Bodale,
    Amoniak,
    Rozok
}