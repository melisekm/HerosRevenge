using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    private AbilityType abilityType = AbilityType.Empty;
    [HideInInspector] public bool isActive;

    void Start()
    {
    }

    public void SetAbilityType(AbilityType abilityT)
    {
        abilityType = abilityT;
    }

    void Update()
    {
        if (!isActive || abilityType == AbilityType.Empty) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ScriptableAbility scriptableAbility = ResourceSystem.Instance.GetAbilityByType(abilityType);
            var abilityPrefab = scriptableAbility.prefab;
            Debug.Log(scriptableAbility.prefab.transform.rotation.x);
            Debug.Log(scriptableAbility.prefab.transform.rotation.y);
            Debug.Log(scriptableAbility.prefab.transform.rotation.z);


            Ability fireOrbAbility = Instantiate(
                abilityPrefab, transform.position,
                Quaternion.identity
            );
            if (fireOrbAbility)
            {
                Debug.Log("Instantiated");
                Debug.Log("Setting Stats");
                fireOrbAbility.SetAbilityStats(scriptableAbility.stats);
                Debug.Log("Stats set");
                Debug.Log(fireOrbAbility.transform.rotation.z);

                fireOrbAbility.enabled = true;
                Debug.Log("Enabled");
                Debug.Log(fireOrbAbility.transform.rotation.z);

            }
        }
    }
}