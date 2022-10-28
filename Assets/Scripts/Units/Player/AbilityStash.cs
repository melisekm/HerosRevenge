using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityStash : MonoBehaviour
{
    private List<AbilityHolder> abilityList = new();
    public TMP_Text selectedAbilityNumber;
    private AbilityHolder selectedAbility;

    private void Start()
    {
        foreach (var abilityHolder in GetComponents<AbilityHolder>())
        {
            abilityList.Add(abilityHolder);
        }

        selectedAbility = abilityList[0];
        abilityList[0].SetAbilityType(AbilityType.FireOrb);
        abilityList[1].SetAbilityType(AbilityType.PiercingShot);
    }

    private void Update()
    {
        bool wasAbilityChanged = false;
        // selection 1 - abilityList.Count
        for (int i = 0; i < abilityList.Count; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                selectedAbilityNumber.text = (i + 1).ToString();
                abilityList[i].isActive = true;
                selectedAbility = abilityList[i];
                wasAbilityChanged = true;
                break;
            }
        }

        if (!wasAbilityChanged) return;
        foreach (AbilityHolder abilityHolder in abilityList)
        {
            if (abilityHolder != selectedAbility)
            {
                abilityHolder.isActive = false;
            }
        }
    }
}