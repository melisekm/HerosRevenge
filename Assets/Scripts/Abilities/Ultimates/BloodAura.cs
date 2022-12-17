using UnityEngine;

public class BloodAura : GroundAbility, IUltimateEventInvokable
{
    private IUltimateEventInvokable ultimateEventInvokable;


    protected override void Start()
    {
        base.Start();
        var player = GameObject.FindWithTag("Player");
        if (player)
        {
            transform.parent = player.transform;
            transform.position = player.transform.position;
        }
        else
        {
            Debug.LogError("Player not found");
        }

        ultimateEventInvokable = this;
    }

    protected override void Update()
    {
        base.Update();
        ultimateEventInvokable.InvokeUltimateEvent(dissapearTime, usedByPlayer);
    }

    private void OnEnable()
    {
        PlayerUnit.OnPlayerDied += Disable;
    }

    private void OnDisable()
    {
        PlayerUnit.OnPlayerDied -= Disable;
    }

    public void OnDestroy()
    {
        ultimateEventInvokable.InvokeUltimateEvent(IUltimateEventInvokable.EndOfActivation, usedByPlayer);
    }

    private void Disable()
    {
        isActive = false;
    }
}