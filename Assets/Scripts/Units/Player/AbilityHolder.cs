using UnityEngine;
using Utils;

[RequireComponent(typeof(PlayerUnit))]
public class AbilityHolder : MonoBehaviour
{
    private AbilityType abilityType = AbilityType.Empty;
    private ScriptableAbility scriptableAbility;
    private PlayerUnit playerUnit;
    [HideInInspector] public bool isHolderActive;
    private float cooldownTime;
    private AbilityState abilityState = AbilityState.Ready;


    private enum AbilityState
    {
        Ready,
        Cooldown
    }

    private void Start()
    {
        playerUnit = GetComponent<PlayerUnit>();
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
            // distance between player and mouseposition
            Vector3 distance = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            // clampf to maxcastrange
            distance = Vector3.ClampMagnitude(distance, playerUnit.maxCastRange);
            ability.Activate(UpdateAbilityStats(), distance, Faction.Enemy);
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
        if (abilityType != AbilityType.Empty && abilityState == AbilityState.Cooldown)
        {
            if (cooldownTime > 0)
                cooldownTime -= Time.deltaTime;
            else
                abilityState = AbilityState.Ready;
        }
    }
}