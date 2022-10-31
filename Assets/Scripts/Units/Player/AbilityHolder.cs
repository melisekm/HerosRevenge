using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    private AbilityType abilityType = AbilityType.Empty;
    private ScriptableAbility scriptableAbility;
    public PlayerUnit playerUnit;
    [HideInInspector] public bool isHolderActive;
    private float cooldownTime;

    private enum AbilityState
    {
        Ready,
        Cooldown
    }

    AbilityState abilityState = AbilityState.Ready;

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
            case AbilityState.Ready:
                if (isHolderActive && Input.GetKeyDown(KeyCode.Space))
                {
                    ActivateAbility(out var ability);
                    abilityState = AbilityState.Cooldown;
                    cooldownTime = ability.abilityStats.baseCooldown;
                }

                break;
            case AbilityState.Cooldown:
                if (cooldownTime > 0)
                    cooldownTime -= Time.deltaTime;
                else
                    abilityState = AbilityState.Ready;
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