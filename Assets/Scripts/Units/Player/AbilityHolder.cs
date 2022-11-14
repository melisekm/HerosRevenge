using UnityEngine;
using Utils;

public class AbilityHolder : MonoBehaviour
{
    private AbilityType abilityType = AbilityType.Empty;
    private ScriptableAbility scriptableAbility;
    public PlayerUnit playerUnit;
    [HideInInspector] public bool isHolderActive;
    private float cooldownTime;
    private AbilityState abilityState = AbilityState.Ready;


    private enum AbilityState
    {
        Ready,
        Cooldown
    }
    
    private void OnEnable()
    {
        PlayerControls.OnAttack += ActivateAbility;
    }

    private void OnDisable()
    {
        PlayerControls.OnAttack -= ActivateAbility;
    }


    private void ActivateAbility()
    {
        if (isHolderActive && abilityType != AbilityType.Empty && abilityState == AbilityState.Ready)
        {
            Ability ability = Instantiate(scriptableAbility.prefab, transform.position, Quaternion.identity);
            ability.Activate(UpdateAbilityStats(), Camera.main.ScreenToWorldPoint(Input.mousePosition), Faction.Enemy);
            abilityState = AbilityState.Cooldown;
            cooldownTime = ability.abilityStats.baseCooldown;
        }
    }

    private AbilityStats UpdateAbilityStats()
    {
        AbilityStats abilityStats = scriptableAbility.stats;
        abilityStats.damage += playerUnit.attributes.attackPower.actual;
        abilityStats.baseCooldown *= 1 - playerUnit.attributes.cooldownRecovery.actual;
        return abilityStats;
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
        if (abilityType !=  AbilityType.Empty && abilityState == AbilityState.Cooldown)
        {
            if (cooldownTime > 0)
                cooldownTime -= Time.deltaTime;
            else
                abilityState = AbilityState.Ready;
        }
    }
}