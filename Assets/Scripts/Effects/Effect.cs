using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    public abstract void Initialize(ScriptableEffect effect, Faction targetFaction);
}