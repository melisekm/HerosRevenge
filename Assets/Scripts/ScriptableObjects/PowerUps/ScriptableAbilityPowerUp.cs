using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/PowerUps/New Scriptable Ability PowerUp")]
public class ScriptableAbilityPowerUp : ScriptablePowerUp
{
    [NonSerialized] public ScriptableAbility ability;
}