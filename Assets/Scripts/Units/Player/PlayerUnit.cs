using System;
using Units.Player;
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

    protected override void Awake()
    {
        base.Awake();
        faction = Faction.Player;
    }

    protected override void OnStateChanged(GameState state)
    {
        if (state == GameState.Playing)
        {
            // this happens before the player movement script is enabled
        }
    }

    protected void Start()
    {
        // TODO TEMPORARY Spawn manager should set this or load it from somewhere
        ScriptablePlayer playerScriptable = ResourceSystem.Instance.player;
        SetAttributes(new Attributes(playerScriptable.attributes));
        SetStats(new PlayerStats(playerScriptable.playerStats));
        progressionController = new ProgressionController(this, levelUpMultiplier, rewardsCount);
        Energy.OnEnergyCollected += progressionController.PickUpEnergy;
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
                    stats.gold.actual += statUpgrade.amount;
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
        GameManager.Instance.ChangeState(GameState.ArenaFailed);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        OnPlayerHealthChanged?.Invoke(attributes.health.actual, attributes.health.initial);
    }
}