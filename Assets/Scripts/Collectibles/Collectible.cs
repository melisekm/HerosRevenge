using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    // how close the game object has to be to disappear after flying towards the player
    public float flySpeed = 10f;
    public bool shouldFlyTowardsPlayer;
    private float destroyDistance = 0.1f;
    protected bool pickedUp;
    private GameObject player;
    protected PlayerUnit playerUnit;

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
        if (playerUnit.attributes.health.actual <= 0) return;

        if (shouldFlyTowardsPlayer && pickedUp)
        {
            // fly towards player
            transform.position = Vector3.MoveTowards(
                transform.position, player.transform.position, flySpeed * Time.deltaTime
            );
            // if reached player destroy self
            if (Vector3.Distance(transform.position, player.transform.position) < destroyDistance)
            {
                OnPlayerReach();
            }
        }
        else
        {
            if (!pickedUp && Vector3.Distance(transform.position, player.transform.position) <
                playerUnit.attributes.pickupRange.actual)
            {
                PickUp();
            }
        }
    }

    protected virtual void OnPlayerReach()
    {
    }


    public virtual void PickUp()
    {
        pickedUp = true;
    }
}