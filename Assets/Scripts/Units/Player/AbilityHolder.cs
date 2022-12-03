using System;
using UnityEngine;

[RequireComponent(typeof(PlayerUnit))]
public class AbilityHolder : MonoBehaviour
{
    public AbilityType abilityType = AbilityType.Empty;
    private PlayerUnit playerUnit;
    [NonSerialized] public bool isHolderActive;
    [NonSerialized] public float cooldownTime;
    [NonSerialized] public ScriptableAbility scriptableAbility;
    private AbilityState abilityState = AbilityState.Ready;
    public event Action OnUltimateUsed;
    public event Action OnAbilityReady;

    private Camera mainCamera;
    // here it would be best to have some kind of container holding info about currently active ability
    // e.g. scriptableability, values, type etc

    private enum AbilityState
    {
        Ready,
        Cooldown
    }

    private void Start()
    {
        mainCamera = Camera.main;
        playerUnit = GetComponent<PlayerUnit>();
    }

    public void ActivateAbility()
    {
        if (isHolderActive && abilityType != AbilityType.Empty && abilityState == AbilityState.Ready)
        {
            Ability ability = Instantiate(scriptableAbility.prefab, transform.position, Quaternion.identity);
            // https://stackoverflow.com/a/67777412
            // clamp magnitude to limit cast range 
            Vector3 mousepos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            var delta = Vector3.ClampMagnitude(mousepos - transform.position, playerUnit.maxCastRange);
            ability.Activate(UpdateAbilityStats(), transform.position + delta, Faction.Enemy);
            abilityState = AbilityState.Cooldown;
            cooldownTime = ability.abilityStats.baseCooldown;
            if (scriptableAbility.group == AbilityGroup.Ultimate)
            {
                OnUltimateUsed?.Invoke();
            }
        }
    }

    private AbilityStats UpdateAbilityStats()
    {
        AbilityStats abilityStats = scriptableAbility.stats;
        abilityStats.damage += playerUnit.attributes.attackPower.actual;
        abilityStats.baseCooldown *= 1 - playerUnit.attributes.cooldownRecovery.actual;
        // can also do something based on player level or level he is *currently* in
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
            {
                cooldownTime -= Time.deltaTime;
            }
            else
            {
                abilityState = AbilityState.Ready;
                OnAbilityReady?.Invoke();
            }
        }
    }
}