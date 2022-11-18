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
            // https://stackoverflow.com/a/67777412
            // clamp magnitude to limit cast range 
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var delta  = Vector3.ClampMagnitude(mousepos - transform.position, playerUnit.maxCastRange);
            ability.Activate(UpdateAbilityStats(), transform.position + delta, Faction.Enemy);
            abilityState = AbilityState.Cooldown;
            cooldownTime = ability.abilityStats.baseCooldown;
        }
    }

    private AbilityStats UpdateAbilityStats()
    {
        AbilityStats abilityStats = scriptableAbility.stats;
        abilityStats.damage += playerUnit.attributes.attackPower.actual;
        abilityStats.baseCooldown *= 1 - playerUnit.attributes.cooldownRecovery.actual;
        // can also do something based on player level or level he is currently in
        // if player is level 7, then increase damage further by 10%
        abilityStats.damage *= 1 + playerUnit.stats.level.actual * 0.1f;
        
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