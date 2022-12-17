using System;
using UnityEngine;

[RequireComponent(typeof(PlayerUnit))]
public class AbilityHolder : MonoBehaviour
{
    public AbilityType abilityType = AbilityType.Empty;
    private AbilityState abilityState = AbilityState.Ready;
    [NonSerialized] public float cooldownTime;
    [NonSerialized] public bool isHolderActive;

    private Camera mainCamera;
    private PlayerUnit playerUnit;
    [NonSerialized] public ScriptableAbility scriptableAbility;

    private void Start()
    {
        mainCamera = Camera.main;
        playerUnit = GetComponent<PlayerUnit>();
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

    public event Action OnUltimateUsed;
    public event Action OnAbilityReady;

    public void ActivateAbility()
    {
        if (isHolderActive && abilityType != AbilityType.Empty && abilityState == AbilityState.Ready)
        {
            Ability ability = Instantiate(scriptableAbility.prefab, transform.position, Quaternion.identity);
            // https://stackoverflow.com/a/67777412
            // clamp magnitude to limit cast range 
            Vector3 mousepos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            var delta = Vector3.ClampMagnitude(mousepos - transform.position, playerUnit.maxCastRange);
            if (ability is MultiProjectileAbility multiProjectileAbility)
            {
                // every N level increase max projectiles by 2(if N=7, 1-6: 1, 7-13: 3, 14-20: 5 etc)
                multiProjectileAbility.numberOfProjectiles =
                    CalculateProjectilesToFire(multiProjectileAbility.numberOfProjectiles);
            }

            ability.Activate(UpdateAbilityStats(), transform.position + delta, Faction.Enemy);
            abilityState = AbilityState.Cooldown;
            cooldownTime = ability.abilityStats.baseCooldown;
            if (scriptableAbility.group == AbilityGroup.Ultimate)
            {
                ability.usedByPlayer = true;
                OnUltimateUsed?.Invoke();
            }
        }
    }

    private int CalculateProjectilesToFire(int abilityCurrentProjectiles)
    {
        // every N level increase max projectiles by 2(if N=7, 1-6: 1, 7-13: 3, 14-20: 5 etc)
        int projectilesBasedOnLevel =
            1 + 2 * (int)(playerUnit.stats.level.actual / playerUnit.levelToIncreaseMaxProjectiles);
        // choose max of ability default projectiles or current projectiles based on level, or global max
        return Math.Min(Math.Max(abilityCurrentProjectiles, projectilesBasedOnLevel),
            playerUnit.globalMaxProjectilesToFire);
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

    private enum AbilityState
    {
        Ready,
        Cooldown
    }
}