using System.Collections.Generic;
using UnityEngine;

public class AbilityStash : MonoBehaviour
{
    private List<AbilityHolder> abilityList = new();
    private AbilityHolder selectedAbility;
    public List<AbilityType> defaultAbilityTypes = new();


    public void OnEnable()
    {
        PlayerControls.OnSwitchAbility += SwitchAbility;
        LevelUpUISetter.OnRewardSelected += SetAbility;
    }

    public void OnDisable()
    {
        PlayerControls.OnSwitchAbility -= SwitchAbility;
        LevelUpUISetter.OnRewardSelected -= SetAbility;
    }

    private void SetAbility(ScriptableReward scriptableAbility)
    {
        // check type of scriptableReward
        if (scriptableAbility is ScriptableAbility ability)
        {
            var randomIndex = Random.Range(0, abilityList.Count);
            var abilityHolder = abilityList[randomIndex];
            abilityHolder.SetAbilityType(ability.abilityType);
            selectedAbility.cooldownTime = 0.5f; // TODO FIXME set cooldown time so it wont fire immediately on click
        }
    }

    private void Start()
    {
        var playerContainer = GameObject.FindWithTag("PlayerContainer").GetComponent<PlayerContainer>();
        for (int i = 0; i < defaultAbilityTypes.Count; i++)
        {
            var abilityHolder = gameObject.AddComponent<AbilityHolder>();
            abilityList.Add(abilityHolder);
        }

        if (playerContainer.abilityList.Count == 0) // if this is first run we set abilities to default
        {
            for (int i = 0; i < defaultAbilityTypes.Count; i++)
            {
                abilityList[i].SetAbilityType(defaultAbilityTypes[i]);
            }
        }
        else
        {
            // else playercontainer already holds abilities,
            // so we just take their types because values are set when ability is activated
            for (int i = 0; i < playerContainer.abilityList.Count; i++)
            {
                abilityList[i].SetAbilityType(playerContainer.abilityList[i].abilityType);
            }
        }

        // save reference so its persistent
        playerContainer.abilityList = abilityList;
        
        selectedAbility = abilityList[0];
        selectedAbility.isHolderActive = true;
    }

    private void SwitchAbility(int index)
    {
        abilityList[index].isHolderActive = true;
        selectedAbility = abilityList[index];

        foreach (AbilityHolder abilityHolder in abilityList)
        {
            if (abilityHolder != selectedAbility)
            {
                abilityHolder.isHolderActive = false;
            }
        }
    }
}