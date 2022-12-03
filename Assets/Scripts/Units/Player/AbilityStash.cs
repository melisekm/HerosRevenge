using System.Collections.Generic;
using UnityEngine;

public class AbilityStash : MonoBehaviour
{
    private List<AbilityHolder> abilityList = new();
    private AbilityHolder selectedAbilityHolder;
    public List<AbilityType> defaultAbilityTypes = new();
    public AbilityHolder ultimateAbilityHolder;


    public void OnEnable()
    {
        PlayerControls.OnSwitchAbility += SwitchAbility;
        PlayerControls.OnUltimateUse += UseUltimate;
        LevelUpUISetter.OnRewardSelected += SetAbility;
    }

    public void OnDisable()
    {
        PlayerControls.OnSwitchAbility -= SwitchAbility;
        PlayerControls.OnUltimateUse -= UseUltimate;
        LevelUpUISetter.OnRewardSelected -= SetAbility;
        foreach (var abilityHolder in abilityList)
        {
            PlayerControls.OnAttack -= abilityHolder.ActivateAbility;
        }
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
                // TODO FIXME set cooldown time so it wont fire immediately on click
                selectedAbilityHolder.cooldownTime = 0.5f;
            }
            else if (ability.group == AbilityGroup.Ultimate)
            {
                ultimateAbilityHolder.SetAbilityType(ability.abilityType);
            }
        }
    }

    private void Start()
    {
        for (int i = 0; i < defaultAbilityTypes.Count; i++)
        {
            var abilityHolder = gameObject.AddComponent<AbilityHolder>();
            PlayerControls.OnAttack += abilityHolder.ActivateAbility;
            abilityList.Add(abilityHolder);
        }

        if (GameObject.FindWithTag("PlayerContainer").TryGetComponent(out PlayerContainer playerContainer))
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
        }

        selectedAbilityHolder = abilityList[0];
        selectedAbilityHolder.isHolderActive = true;
        ultimateAbilityHolder.isHolderActive = true;
    }

    private void UseUltimate()
    {
        ultimateAbilityHolder.ActivateAbility();
    }


    private void SwitchAbility(int index)
    {
        abilityList[index].isHolderActive = true;
        selectedAbilityHolder = abilityList[index];

        foreach (AbilityHolder abilityHolder in abilityList)
        {
            if (abilityHolder != selectedAbilityHolder)
            {
                abilityHolder.isHolderActive = false;
            }
        }
    }
}