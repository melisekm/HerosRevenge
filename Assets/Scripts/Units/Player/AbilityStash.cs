using System;
using System.Collections.Generic;
using Units.Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class AbilityStash : MonoBehaviour
{
    private List<AbilityHolder> abilityList = new();
    private AbilityHolder selectedAbilityHolder;
    public List<AbilityType> defaultAbilityTypes = new();
    public AbilityHolder ultimateAbilityHolder;
    public static event Action<ScriptableAbility, string> OnAbilityChanged;

    public void OnEnable()
    {
        PlayerControls.OnSwitchAbility += SwitchAbility;
        PlayerControls.OnUltimateButtonPress += TryToUseUltimate;
        LevelUpUISetter.OnRewardSelected += SetAbility;
        // we need this because after clicking on reward it would immediately activate ability
        ProgressionController.OnLevelUp += DisableSelectedHolder;
    }

    public void OnDisable()
    {
        PlayerControls.OnSwitchAbility -= SwitchAbility;
        PlayerControls.OnUltimateButtonPress -= TryToUseUltimate;
        LevelUpUISetter.OnRewardSelected -= SetAbility;
        foreach (var abilityHolder in abilityList)
        {
            PlayerControls.OnAttack -= abilityHolder.ActivateAbility;
        }

        ProgressionController.OnLevelUp -= DisableSelectedHolder;
    }

    private void Start()
    {
        for (int i = 0; i < defaultAbilityTypes.Count; i++)
        {
            var abilityHolder = gameObject.AddComponent<AbilityHolder>();
            PlayerControls.OnAttack += abilityHolder.ActivateAbility;
            abilityList.Add(abilityHolder);
        }

        var playerContainerObj = GameObject.FindWithTag("PlayerContainer");
        if (playerContainerObj && playerContainerObj.TryGetComponent(out PlayerContainer playerContainer))
        {
            if (playerContainer.abilityTypes.Count == 0) // if this is first run we set abilities to default
            {
                for (int i = 0; i < defaultAbilityTypes.Count; i++)
                {
                    abilityList[i].SetAbilityType(defaultAbilityTypes[i]);
                }

                ultimateAbilityHolder.SetAbilityType(ultimateAbilityHolder.abilityType);
            }
            else
            {
                // else playercontainer already holds abilities,
                // so we just take their types because values are set when ability is activated
                for (int i = 0; i < playerContainer.abilityTypes.Count; i++)
                {
                    abilityList[i].SetAbilityType(playerContainer.abilityTypes[i]);
                }

                ultimateAbilityHolder.SetAbilityType(playerContainer.ultimateType);
            }

            OnAbilityChanged?.Invoke(ultimateAbilityHolder.scriptableAbility, "R");
            OnAbilityChanged?.Invoke(abilityList[0].scriptableAbility, "1");
            OnAbilityChanged?.Invoke(abilityList[1].scriptableAbility, "2");
        }

        selectedAbilityHolder = abilityList[0];
        selectedAbilityHolder.isHolderActive = true;
        ultimateAbilityHolder.isHolderActive = true;
    }

    public void OnDestroy()
    {
        // save abilities
        var playerContainerObj = GameObject.FindWithTag("PlayerContainer");
        if (playerContainerObj && playerContainerObj.TryGetComponent(out PlayerContainer playerContainer))
        {
            playerContainer.abilityTypes = abilityList.ConvertAll(ability => ability.abilityType);
            playerContainer.ultimateType = ultimateAbilityHolder.abilityType;
        }
    }

    private void DisableSelectedHolder(PlayerStats _, Attributes __, bool initial, RewardGenerator.Reward[] ___)
    {
        if (!initial) selectedAbilityHolder.isHolderActive = false;
    }

    private void SetAbility(ScriptableReward scriptableAbility)
    {
        // check type of scriptableReward
        if (scriptableAbility is ScriptableAbility ability)
        {
            if (ability.group == AbilityGroup.Regular)
            {
                var randomIndex = Random.Range(0, abilityList.Count);
                var abilityHolder = abilityList[randomIndex];
                abilityHolder.SetAbilityType(ability.abilityType);
                OnAbilityChanged?.Invoke(abilityHolder.scriptableAbility, (randomIndex + 1).ToString());
            }
            else if (ability.group == AbilityGroup.Ultimate)
            {
                ultimateAbilityHolder.SetAbilityType(ability.abilityType);
                OnAbilityChanged?.Invoke(ultimateAbilityHolder.scriptableAbility, "R");
            }
        }

        // we have disabled selected ability holder so it doesnt fire immediately
        // see OnEnable and ProgressionController.OnLevelUp += DisableSelectedHolder;
        selectedAbilityHolder.isHolderActive = true;
    }

    private void TryToUseUltimate()
    {
        ultimateAbilityHolder.ActivateAbility();
    }

    private void SwitchAbility(int index)
    {
        selectedAbilityHolder.isHolderActive = false;
        abilityList[index].isHolderActive = true;
        selectedAbilityHolder = abilityList[index];
    }
}