using System.Reflection;
using UnityEngine;
using Utils;

public class PlayerUnit : Unit
{
    public PlayerStats stats { get; private set; }
    public void SetStats(PlayerStats st) => stats = st;
    public float pickupRange = 2f;
    public float maxCastRange = 10f;
    public float levelUpMultiplier = 1.25f;

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

    public void PickUpEnergy(float amount)
    {
        stats.xp.actual += amount;
        if (stats.xp.actual >= stats.xp.max && stats.level.actual < stats.level.max)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        // TODO show Level up screen
        // TODO add level up effect
        // TODO add level up sound
        stats.xp.actual = stats.xp.min;
        stats.xp.max = Mathf.RoundToInt(stats.xp.max * levelUpMultiplier);
        stats.level.actual++;
        stats.gold.actual += stats.gold.increasePerLevel;


        LevelUpAttribute(attributes.health);
        LevelUpAttribute(attributes.speed);
        LevelUpAttribute(attributes.attackPower);
        LevelUpAttribute(attributes.cooldownRecovery);
        LevelUpAttribute(attributes.defenseRating);
    }

    private void LevelUpAttribute(Attribute attr)
    {
        float newValue = attr.initial + attr.increasePerLevel;
        if (newValue < attr.max)
        {
            attr.initial = newValue;
            attr.actual = attr.initial;
        }
        else
        {
            attr.initial = attr.max;
            attr.actual = attr.max;
        }
    }
}