using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    protected bool pickedUp;
    protected GameObject player;
    private PlayerUnit playerUnit;
    protected SpriteRenderer spriteRenderer;

    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerUnit = player.GetComponent<PlayerUnit>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        // cant pickup if player is dead
        if (pickedUp || playerUnit.attributes.health.actual <= 0) return;

        if (Vector3.Distance(transform.position, player.transform.position) < playerUnit.attributes.pickupRange.actual)
        {
            pickedUp = true;
            PickUp();
        }
    }


    public abstract void PickUp();
}