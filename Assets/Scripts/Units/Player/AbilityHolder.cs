using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    private AbilityType abilityType = AbilityType.Empty;
    private ScriptableAbility scriptableAbility;
    public PlayerUnit playerUnit;
    [HideInInspector] public bool isHolderActive;
    private float cooldownTime;

    enum AbilityState
    {
        ready,
        cooldown
    }

    AbilityState abilityState = AbilityState.ready;

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

    private void Update()
    {
        if (abilityType == AbilityType.Empty) return;

        switch (abilityState)
        {
            case AbilityState.ready:
                if (isHolderActive && Input.GetKeyDown(KeyCode.Space))
                {
                    ActivateAbility(out var ability);
                    abilityState = AbilityState.cooldown;
                    cooldownTime = ability.abilityStats.baseCooldown;
                }

                break;
            case AbilityState.cooldown:
                if (cooldownTime > 0)
                    cooldownTime -= Time.deltaTime;
                else
                    abilityState = AbilityState.ready;
                break;
        }
    }

    private void ActivateAbility(out Ability ability)
    {
        ability = Instantiate(scriptableAbility.prefab, transform.position, Quaternion.identity);
        AbilityStats abilityStats = scriptableAbility.stats;
        abilityStats.damage += playerUnit.attributes.attackPower.actual;
        abilityStats.baseCooldown = abilityStats.baseCooldown * (1 - playerUnit.attributes.cooldownRecovery.actual);
        ability.SetAbilityStats(abilityStats);
        ability.enabled = true;
    }
}