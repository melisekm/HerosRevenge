using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Energy : Collectible
{
    public int amount;
    public float flySpeed = 10f;
    
    // how close the game object has to be to disappear after flying towards the player
    private float destroyDistance = 0.1f;

    private bool pickedUp;


    protected override void PickUp()
    {
        if (player && player.TryGetComponent(out PlayerUnit playerUnit))
        {
            playerUnit.PickUpEnergy(amount);
            pickedUp = true;
        }
    }

    protected override void Update()
    {
        if (pickedUp)
        {
            // fly towards player
            transform.position = Vector3.MoveTowards(
                transform.position, player.transform.position, flySpeed * Time.deltaTime
            );
            // if reached player destroy self
            if (Vector3.Distance(transform.position, player.transform.position) < destroyDistance)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            base.Update();
        }
    }
}