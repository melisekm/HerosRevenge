using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    protected GameObject player;
    private PlayerUnit playerUnit;
    protected bool pickedUp;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerUnit = player.GetComponent<PlayerUnit>();
    }

    protected virtual void Update()
    {
        // cant pickup if player is dead
        if (pickedUp || playerUnit.attributes.health.actual <= 0) return;
        
        if (Vector3.Distance(transform.position, player.transform.position) < playerUnit.attributes.pickupRange.actual)
        {
            PickUp();
        }
    }

    protected abstract void PickUp();
}
