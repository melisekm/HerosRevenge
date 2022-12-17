using System;
using UnityEngine;

public class ScriptableEffect : ScriptableObject
{
    public EffectType effectType;
    public Effect effect;
}

[Serializable]
public enum EffectType
{
    DangerIndicator
}