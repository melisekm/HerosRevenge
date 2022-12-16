using System;
using UnityEngine;

public class Energy : Collectible
{
    public int amount;
    public float flySpeed = 10f;

    // how close the game object has to be to disappear after flying towards the player
    private float destroyDistance = 0.1f;

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
                OnEnergyCollected?.Invoke(amount);
                Destroy(gameObject);
            }
        }

        base.Update();
    }

    public static event Action<int> OnEnergyCollected;

    public override void PickUp()
    {
        pickedUp = true;
    }
}