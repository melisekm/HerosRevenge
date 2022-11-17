using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public abstract class Effect : MonoBehaviour
{
    public abstract void Initialize(ScriptableEffect effect, Faction targetFaction);
}
