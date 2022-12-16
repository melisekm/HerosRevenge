using System;
using Units.Player;
using UnityEngine;

public class PlayerUnit : Unit
{
    [Tooltip("Max cast range for spells which do are not projectiels (e.g. how far can player place PoisonRain")]
    public float maxCastRange = 10f;

    [Tooltip("How much xp is needed to level up. (increase per level)")]
    public float levelUpMultiplier = 1.25f;

    [Tooltip("How many rewards are when leveling up")] // needs a lot more additonal changes
    public int rewardsCount = 2;

    [Tooltip("At which level player will get more projectiles per multiproj ability")]
    public int levelToIncreaseMaxProjectiles = 7;

    [Tooltip("Global maximum even if player is above levelToIncreaseMaxProjectiles")]
    public int globalMaxProjectilesToFire = 9;

    public PlayerStats stats { get; private set; }

    private ProgressionController progressionController { get; set; }

    protected override void Awake()
    {
        base.Awake();
        faction = Faction.Player;
        var playerContainerObj = GameObject.FindWithTag("PlayerContainer");
        if (playerContainerObj && playerContainerObj.TryGetComponent(out PlayerContainer playerContainer))
            Initialize(playerContainer);
        else
            Debug.LogWarning("PlayerContainer not found");
        var abilityStash = GetComponent<AbilityStash>();
        progressionController = new ProgressionController(this, abilityStash);
    }

    protected void OnEnable()
    {
        Energy.OnEnergyCollected += progressionController.PickUpEnergy;
        Treasure.OnTreasueCollected += progressionController.PickUpGold;
    }

    private void OnDisable()
    {
        Energy.OnEnergyCollected -= progressionController.PickUpEnergy;
        Treasure.OnTreasueCollected -= progressionController.PickUpGold;
    }

    public static event Action OnPlayerDied;

    protected override void Die()
    {
        sprite.enabled = false;
        stats.gold.actual = 0; // he lost all his gold when he died
        stats.xp.actual = 0; // and xp too
        // TODO: add death animation
        OnPlayerDied?.Invoke();
    }

    private void Initialize(PlayerContainer playerContainer)
    {
        SetAttributes(playerContainer.playerAttributes);
        stats = playerContainer.playerStats;
        attributes.health.actual = attributes.health.initial; // set health to max incase it was changed
    }
}