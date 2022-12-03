using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    protected GameObject player;
    private PlayerUnit playerUnit;
    protected bool pickedUp;

    public abstract void PickUp();

    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player");
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
}