using UnityEngine;

public class EnergyVacuum : Ability, IUltimateEventInvokable
{
    private IUltimateEventInvokable ultimateEventInvokable;

    private void Start()
    {
        var energies = GameObject.FindGameObjectsWithTag("Energy");
        foreach (var energy in energies)
        {
            var energyComp = energy.GetComponent<Energy>();
            energyComp.PickUp();
        }

        ultimateEventInvokable = this;
        ultimateEventInvokable.InvokeUltimateEvent(IUltimateEventInvokable.EndOfActivation, usedByPlayer);
        Destroy(gameObject);
    }
}