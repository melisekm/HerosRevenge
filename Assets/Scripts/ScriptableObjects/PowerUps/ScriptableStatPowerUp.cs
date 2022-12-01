using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/PowerUps/New Scriptable Stat PowerUp")]
public class ScriptableStatPowerUp : ScriptablePowerUp
{
    [NonSerialized] public ScriptableStatUpgrade statUpgrade;
    [Tooltip("Use random range amount or set amount of stat upgrade that it receives")]
    public bool randomAmount = true;
    [Header("If Random Amount is true")]
    public int minRandomAmount;
    public int maxRandomAmount;
}
