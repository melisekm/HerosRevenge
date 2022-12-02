using System;
using Units.Player;
using UnityEngine;
using Utils;

public class PlayerUnit : Unit
{
    public PlayerStats stats { get; private set; }
    public float maxCastRange = 10f;
    public float levelUpMultiplier = 1.25f;
    public int rewardsCount = 2;
    public ProgressionController progressionController { get; private set; }
    private AttributeUpgrader attributeUpgrader;

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
        attributeUpgrader = new AttributeUpgrader(this);
        Energy.OnEnergyCollected += progressionController.PickUpEnergy;
        Treasure.OnTreasueCollected += progressionController.PickUpGold;
        LevelUpUISetter.OnRewardSelected += UpdateAttributes;
        OnPlayerHealthChanged?.Invoke(attributes.health.actual, attributes.health.initial);
    }

    private void UpdateAttributes(ScriptableReward scriptableStat)
    {
        if (scriptableStat is ScriptableStatUpgrade statUpgrade)
        {
            attributeUpgrader.Upgrade(statUpgrade.statType, statUpgrade.amount);
            if (statUpgrade.statType == StatType.Health)
            {
                OnPlayerHealthChanged?.Invoke(attributes.health.actual, attributes.health.initial);
            }
        }
    }

    private void OnDisable()
    {
        Energy.OnEnergyCollected -= progressionController.PickUpEnergy;
        LevelUpUISetter.OnRewardSelected -= UpdateAttributes;
        Treasure.OnTreasueCollected -= progressionController.PickUpGold;
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
        stats = playerContainer.playerStats;
        attributes.health.actual = attributes.health.initial; // set health to max incase it was changed
    }
}