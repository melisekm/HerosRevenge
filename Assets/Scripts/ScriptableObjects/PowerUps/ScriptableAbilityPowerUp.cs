using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/PowerUps/New Scriptable Ability PowerUp")]
public class ScriptableAbilityPowerUp : ScriptablePowerUp
{
    [HideInInspector] public ScriptableAbility ability;
}
