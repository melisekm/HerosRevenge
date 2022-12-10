using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    public abstract void Initialize(ScriptableEffect effect, float radius, Faction targetFaction);
}