using System;

public interface IUltimate
{
    public float timer { get; set; }
    public static event Action<float> OnUltimateActive;
    public void ActivateUltimate()
    {
        OnUltimateActive?.Invoke(timer);
    }
}