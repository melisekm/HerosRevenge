using System;

public interface IUltimateEventInvokable
{
    public const float EndOfActivation = 0;

    public static event Action<float> OnUltimateActive;

    public void InvokeUltimateEvent(float duration, bool usedByPlayer)
    {
        if (!usedByPlayer) return;
        OnUltimateActive?.Invoke(duration);
    }
}