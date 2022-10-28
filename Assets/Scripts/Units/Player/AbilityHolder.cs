using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    private AbilityType abilityType = AbilityType.Empty;
    private ScriptableAbility scriptableAbility;
    [HideInInspector] public bool isActive;

    void Start()
    {
    }

    public void SetAbilityType(AbilityType abilityT)
    {
        abilityType = abilityT;
        if (abilityType != AbilityType.Empty)
        {
            scriptableAbility = ResourceSystem.Instance.GetAbilityByType(abilityType);
        }
    }

    void Update()
    {
        if (!isActive || abilityType == AbilityType.Empty) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Ability fireOrbAbility = Instantiate(scriptableAbility.prefab, transform.position, Quaternion.identity);
            if (fireOrbAbility)
            {
                fireOrbAbility.SetAbilityStats(scriptableAbility.stats);
                fireOrbAbility.enabled = true;
            }
        }
    }
}