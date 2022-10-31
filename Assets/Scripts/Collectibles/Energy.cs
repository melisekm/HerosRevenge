using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : Collectible
{
    public int amount;

    protected override void PickUp()
    {
        if (player && player.TryGetComponent(out PlayerUnit playerUnit))
        {
            playerUnit.PickUpEnergy(amount);
        }
    }
}
