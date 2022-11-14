using System.Collections.Generic;
using UnityEngine;

public class AbilityStash : MonoBehaviour
{
    private List<AbilityHolder> abilityList = new();
    private AbilityHolder selectedAbility;
    public int maxAbilityCount = 2;


    public void OnEnable()
    {
        PlayerControls.OnSwitchAbility += SwitchAbility;
    }

    public void OnDisable()
    {
        PlayerControls.OnSwitchAbility -= SwitchAbility;
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
        abilityList[0].SetAbilityType(AbilityType.Cleave);
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