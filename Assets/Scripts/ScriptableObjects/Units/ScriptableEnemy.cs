using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Units/New Scriptable Enemy")]
public class ScriptableEnemy : ScriptableUnit
{
    public EnemyType enemyType;
    public int energyDropAmount;
}

[Serializable]
public enum EnemyType
{
    Cpavok,
    Ded,
    Vermener,
    Amoniak,
    Rozok,
    Archimedes
}