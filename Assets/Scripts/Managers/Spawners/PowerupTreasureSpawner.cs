using System.Collections;
using System.Collections.Generic;
using Units.Player;
using UnityEngine;

public class PowerupTreasureSpawner : MonoBehaviour
{
    private RewardGenerator rewardGenerator;
    public List<ScriptableAbility> abilitiesToSpawnAsPowerups;
    public List<ScriptableStatUpgrade> statUpgradesToSpawnAsPowerups;
    
    private void Start()
    {
        rewardGenerator = new RewardGenerator(1);
    }

    private void Update()
    {
        
    }
}
