using UnityEngine;

public class EnergyVacuum : Ability
{
    private void Start()
    {
        var energies = GameObject.FindGameObjectsWithTag("Energy");
        foreach (var energy in energies)
        {
            var energyComp = energy.GetComponent<Energy>();
            energyComp.PickUp();
        }

        Destroy(gameObject);
    }
}