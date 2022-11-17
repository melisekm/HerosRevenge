using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Effects/New Scriptable Effect")]
public class DangerIndicatorEffect : ScriptableEffect
{
    public Ability ability;
    public float damage;
}
