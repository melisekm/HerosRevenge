using System.Collections.Generic;
using UnityEngine;

public class AbilityStash : MonoBehaviour
{
    private List<AbilityHolder> abilityList = new();
    private AbilityHolder selectedAbility;

    public void OnEnable()
    {
        PlayerControls.OnSwitchAbility += SwitchAbility;
    }

    public void OnDisable()
    {
        PlayerControls.OnSwitchAbility -= SwitchAbility;
    }

    private void Start()
    {
        foreach (var abilityHolder in GetComponents<AbilityHolder>())
        {
            abilityList.Add(abilityHolder);
        }

        selectedAbility = abilityList[0];
        selectedAbility.isHolderActive = true;
        abilityList[0].SetAbilityType(AbilityType.FireOrb);
        abilityList[1].SetAbilityType(AbilityType.PiercingShot);
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