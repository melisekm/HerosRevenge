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

    public static event Action OnPlayerDied;

    protected override void Awake()
    {
        base.Awake();
        faction = Faction.Player;
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

    protected override void Die()
    {
        sprite.enabled = false;
        stats.gold.actual = 0; // he lost all his gold when he died
        stats.xp.actual = 0; // and xp too
        // TODO: add death animation
        OnPlayerDied?.Invoke();
    }

    public void Initialize(PlayerContainer playerContainer)
    {
        SetAttributes(playerContainer.playerAttributes);
        stats = playerContainer.playerStats;
        attributes.health.actual = attributes.health.initial; // set health to max incase it was changed
    }
}