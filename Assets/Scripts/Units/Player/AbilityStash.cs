using System.Collections.Generic;
using UnityEngine;

public class AbilityStash : MonoBehaviour
{
    private List<AbilityHolder> abilityList = new();
    private AbilityHolder selectedAbility;
    public int maxAbilityCount = 4;


    public void OnEnable()
    {
        PlayerControls.OnSwitchAbility += SwitchAbility;
        LevelUpUISetter.OnRewardSelected += SetAbility;
    }

    private void SetAbility(ScriptableReward scriptableAbility)
    {
        // check type of reward
        if (scriptableAbility is ScriptableAbility ability)
        {
            var randomIndex = Random.Range(0, abilityList.Count);
            var abilityHolder = abilityList[randomIndex];
            abilityHolder.SetAbilityType(ability.abilityType);
            selectedAbility.cooldownTime = 0.5f; // set cooldown time so it wont fire immediately on click
        }
       
    }

    public void OnDisable()
    {
        PlayerControls.OnSwitchAbility -= SwitchAbility;
        LevelUpUISetter.OnRewardSelected -= SetAbility;
    }

    private void Awake()
    {
        for (int i = 0; i < maxAbilityCount; i++)
        {
            var abilityHolder = gameObject.AddComponent<AbilityHolder>();
            abilityList.Add(abilityHolder);
        }
    }

    private void Start()
    {
        selectedAbility = abilityList[0];
        selectedAbility.isHolderActive = true;
        abilityList[0].SetAbilityType(AbilityType.FireOrb);
        abilityList[1].SetAbilityType(AbilityType.PoisonRain);
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