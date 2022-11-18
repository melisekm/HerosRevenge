using System;
using Units.Player;
using Utils;

public class PlayerUnit : Unit
{
    public PlayerStats stats { get; private set; }
    public void SetStats(PlayerStats st) => stats = st;
    public float pickupRange = 2f;
    public float maxCastRange = 10f;
    public float levelUpMultiplier = 1.25f;
    public int rewardsCount = 2;
    private ProgressionController progressionController;

    public static event Action<float, float> OnPlayerTakeDamage;

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
        OnPlayerTakeDamage?.Invoke(attributes.health.actual, attributes.health.initial);

    }

    private void OnDisable()
    {
        Energy.OnEnergyCollected -= progressionController.PickUpEnergy;
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
        OnPlayerTakeDamage?.Invoke(attributes.health.actual, attributes.health.initial);
    }
}