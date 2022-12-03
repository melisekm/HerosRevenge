using UnityEngine;
using Utils;

public abstract class Effect : MonoBehaviour
{
    public abstract void Initialize(ScriptableEffect effect, Faction targetFaction);
}