using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Units/New Scriptable Enemy")]
public class ScriptableEnemy : ScriptableUnit
{
    public EnemyType enemyType;

}

[Serializable]
public enum EnemyType
{
    Cpavok,
    Ded,
    Vermener,
    Amoniak,
    Rozok
}