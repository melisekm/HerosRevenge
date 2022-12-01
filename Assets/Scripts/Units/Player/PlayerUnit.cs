using System;
using Units.Player;
using UnityEngine;
using Utils;

public class PlayerUnit : Unit
{
    public PlayerStats stats { get; private set; }
    public void SetStats(PlayerStats st) => stats = st;
    public float maxCastRange = 10f;
    public float levelUpMultiplier = 1.25f;
    public int rewardsCount = 2;
    private ProgressionController progressionController;

    public static event Action<float, float> OnPlayerHealthChanged;
    public static event Action OnPlayerDied;

    protected override void Awake()
    {
        base.Awake();
        faction = Faction.Player;
    }

    protected void Start()
    {
        var playerContainerObj = GameObject.FindWithTag("PlayerContainer");
        if (playerContainerObj && playerContainerObj.TryGetComponent(out PlayerContainer playerContainer))
        {
            Initialize(playerContainer);
        }
        else
        {
            Debug.LogWarning("PlayerContainer not found");
        }

        progressionController = new ProgressionController(this, levelUpMultiplier, rewardsCount);
        Energy.OnEnergyCollected += progressionController.PickUpEnergy;
        Treasure.OnTreasueCollected += progressionController.PickUpGold;
        LevelUpUISetter.OnRewardSelected += UpdateAttributes;
        OnPlayerHealthChanged?.Invoke(attributes.health.actual, attributes.health.initial);
    }

    private void UpdateAttributes(ScriptableReward scriptableStat)
    {
        if (scriptableStat is ScriptableStatUpgrade statUpgrade)
        {
            switch (statUpgrade.statType)
            {
                case StatType.Health:
                    attributes.health.initial += statUpgrade.amount;
                    attributes.health.actual += statUpgrade.amount;
                    OnPlayerHealthChanged?.Invoke(attributes.health.actual, attributes.health.initial);
                    break;
                case StatType.Damage:
                    attributes.attackPower.initial += statUpgrade.amount;
                    attributes.attackPower.actual += statUpgrade.amount;
                    break;
                case StatType.Speed:
                    attributes.speed.initial += statUpgrade.amount;
                    attributes.speed.actual += statUpgrade.amount;
                    break;
                case StatType.Defense:
                    attributes.defenseRating.initial += statUpgrade.amount;
                    attributes.defenseRating.actual += statUpgrade.amount;
                    break;
                case StatType.CooldownRecovery:
                    attributes.cooldownRecovery.initial += statUpgrade.amount;
                    attributes.cooldownRecovery.actual += statUpgrade.amount;
                    break;
                case StatType.PickupRange:
                    attributes.pickupRange.initial += statUpgrade.amount;
                    attributes.pickupRange.actual += statUpgrade.amount;
                    break;
                case StatType.Gold:
                    progressionController.PickUpGold((int)statUpgrade.amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void OnDisable()
    {
        Energy.OnEnergyCollected -= progressionController.PickUpEnergy;
        LevelUpUISetter.OnRewardSelected -= UpdateAttributes;
        Treasure.OnTreasueCollected -= progressionController.PickUpGold;
    }

    protected void Update()
    {
        SetFacingDirection();
    }

    private void SetFacingDirection()
    {
        if (rb.velocity.x >= 0.01f)
        {
            if (!isFacingRight)
            {
                Flip();
            }
        }
        else if (rb.velocity.x <= -0.01f)
        {
            if (isFacingRight)
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        sprite.flipX = !sprite.flipX;
        // this only flips sprite, not the collider etc
    }

    protected override void Die()
    {
        sprite.enabled = false;
        stats.gold.actual = 0; // he lost all his gold when he died
        stats.xp.actual = 0; // and xp too
        // TODO: add death animation
        OnPlayerDied?.Invoke();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        OnPlayerHealthChanged?.Invoke(attributes.health.actual, attributes.health.initial);
    }

    public void Initialize(PlayerContainer playerContainer)
    {
        SetAttributes(playerContainer.playerAttributes);
        SetStats(playerContainer.playerStats);
        attributes.health.actual = attributes.health.initial; // set health to max incase it was changed
    }
}