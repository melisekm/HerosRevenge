﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Units.Player
{
    public class ProgressionController
    {
        private PlayerStats playerStats;
        private Attributes playerAttributes;
        private PlayerUnit playerUnit;
        private AbilityStash playerAbilityStash;
        public static event Action<PlayerStats, Attributes, bool, RewardGenerator.Reward[]> OnLevelUp;
        private RewardGenerator rewardGenerator;
        private List<Attribute> attributesToLevelUp;

        public ProgressionController(PlayerUnit playerUnit, AbilityStash abilityStash)
        {
            this.playerUnit = playerUnit;
            playerStats = playerUnit.stats;
            playerAttributes = playerUnit.attributes;
            playerAbilityStash = abilityStash;
            OnLevelUp?.Invoke(playerStats, playerAttributes, true, null);
            rewardGenerator = new RewardGenerator(playerUnit.rewardsCount);
            attributesToLevelUp = new List<Attribute>
            {
                playerAttributes.health,
                playerAttributes.speed,
                playerAttributes.attackPower,
                playerAttributes.cooldownRecovery,
                playerAttributes.defenseRating,
                playerAttributes.pickupRange
            };
        }

        public void PickUpEnergy(int amount)
        {
            if (playerAttributes.health.actual <= 0) return;

            playerStats.xp.actual += amount;
            if (playerStats.xp.actual >= playerStats.xp.max && playerStats.level.actual < playerStats.level.max)
            {
                LevelUp();
            }
        }

        public void PickUpGold(float amount)
        {
            playerStats.gold.actual += amount;
        }

        private void LevelUp()
        {
            playerStats.xp.actual = playerStats.xp.min;
            playerStats.xp.max = Mathf.RoundToInt(playerStats.xp.max * playerUnit.levelUpMultiplier);
            playerStats.level.actual++;
            playerStats.gold.actual += playerStats.gold.increasePerLevel;

            attributesToLevelUp.ForEach(attribute => attribute.LevelUp());

            // get current ability types
            var currentAbilities = playerAbilityStash.abilityList
                .Select(abilityHolder => abilityHolder.abilityType)
                .ToList();

            var abilities = ResourceSystem.Instance.abilities
                .Where(ability => ability.minLevel <= playerStats.level.actual // ability is available for this level
                                  // player doesnt have this ability as ultimate
                                  && playerAbilityStash.ultimateAbilityHolder.abilityType != ability.abilityType
                                  // player doesnt have this ability
                                  && !currentAbilities.Contains(ability.abilityType))
                .ToList();
            var statUpgrades = ResourceSystem.Instance.statUpgrades.ToList();


            OnLevelUp?.Invoke(playerStats, playerAttributes, false,
                rewardGenerator.GenerateRewards(abilities, statUpgrades));
        }
    }
}