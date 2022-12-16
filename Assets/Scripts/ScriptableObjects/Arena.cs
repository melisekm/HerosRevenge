using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Arenas/New Arena")]
public class Arena : ScriptableObject
{
    public string sceneName;
    public List<Arena> mustCompleteArenas;
    public float enemyPowerMultiplier = 1f;
}