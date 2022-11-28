using System;
using System.Collections.Generic;
using System.Linq;
using Units.Player;
using UnityEngine;

public class PowerupTreasureSpawner : GroundEntitySpawner
{
    private RewardGenerator rewardGenerator;
    public List<AbilityType> abilityTypes;
    public List<StatType> statTypes;
    private List<ScriptableAbility> abilities;
    private List<ScriptableStatUpgrade> statUpgrades;

    private void Start()
    {
        rewardGenerator = new RewardGenerator(1);
        abilities = ResourceSystem
            .Instance
            .abilities
            .Where(a => abilityTypes.Contains(a.abilityType))
            .ToList();
        statUpgrades = ResourceSystem
            .Instance
            .statUpgrades
            .Where(s => statTypes.Contains(s.statType))
            .ToList();
    }


    protected override void SpawnGroundEntity(ScriptableObject entity)
    {
        if (entity is ScriptablePowerUp scriptablePowerUp)
        {
            var obj = Instantiate(scriptablePowerUp.prefab, position, Quaternion.identity);
            switch (scriptablePowerUp.powerUpType)
            {
                case PowerUpType.Treasure:
                    var treasure = obj.GetComponent<Treasure>();
                    treasure.Initialize((ScriptableStatPowerUp)scriptablePowerUp);
                    break;
                case PowerUpType.Ability:
                    break;
                case PowerUpType.Stat:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            // maybe we can initialize it (give it some kind of description and value)
        }
    }

    // TODO: Refactor someday
    // Unfortunately I wanted to be clever and have spawn effects, powerups, treasures anything
    // that spawns on the ground as generic as possible so there is a lot of conversion going on
    // also reward statupgrades are merged with potential powerups 
    // what reward generator gives us is the same what happens on level up so it chooses based on input of 
    // abilities and statupgrades, here it is given by editor as two lists, for example we can add ultimate as a powerup
    // then it starts converting it to scriptablepowerup which has reference to the original statupgrade/ability
    protected override ScriptableObject GetEntity()
    {
        var reward = rewardGenerator.GenerateRewards(abilities, statUpgrades).First();
        // well you have to make sure that in list of possible generated rewards
        // aka powerups you add the powerup you want to spawn.. atleast one stat upgrade should be there
        switch (reward.rewardType)
        {
            case RewardGenerator.RewardType.Ability:
                return ConvertToScriptableAbilityPowerUp(reward);
            case RewardGenerator.RewardType.Stat:
                return ConvertToScriptableStatPowerUp(reward);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private ScriptableObject ConvertToScriptableStatPowerUp(RewardGenerator.Reward reward)
    {
        var rewardStatUpgrade = reward.scriptableReward as ScriptableStatUpgrade;
        // do any stat amount calculations here for example based on arena or random etc
        ScriptableStatPowerUp scriptableStatPowerUp;
        if (rewardStatUpgrade.statType == StatType.Gold)
        {
            scriptableStatPowerUp = ResourceSystem.Instance
                .GetPowerUpByType(PowerUpType.Treasure) as ScriptableStatPowerUp;
        }
        else
        {
            scriptableStatPowerUp = ResourceSystem.Instance
                .GetPowerUpByType(PowerUpType.Stat) as ScriptableStatPowerUp;
        }

        scriptableStatPowerUp.statUpgrade = rewardStatUpgrade;
        return scriptableStatPowerUp;
    }

    private ScriptableObject ConvertToScriptableAbilityPowerUp(RewardGenerator.Reward reward)
    {
        var scriptableAbilityPowerUp = ResourceSystem.Instance.GetPowerUpByType(PowerUpType.Ability) as ScriptableAbilityPowerUp;
        scriptableAbilityPowerUp.ability = reward.scriptableReward as ScriptableAbility;
        return scriptableAbilityPowerUp;
    }
}