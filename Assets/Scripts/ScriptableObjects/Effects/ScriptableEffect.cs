using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableEffect : ScriptableObject
{
    public EffectType effectType;
    public Effect effect;
}

[Serializable]
public enum EffectType
{
    Damage,
}