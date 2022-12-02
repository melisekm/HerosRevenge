using UnityEngine;

[RequireComponent(typeof(PlayerUnit))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public bool canMove = true;
    private Attribute speed;
    private PlayerUnit playerUnit;
    private Rigidbody2D rb;
    private Vector2 direction;
    
    private void Start()
    {
        playerUnit = GetComponent<PlayerUnit>();
        rb = GetComponent<Rigidbody2D>();
        speed = playerUnit.attributes.speed;
    }
    

    public void OnEnable()
    {
        PlayerControls.OnMovement += Move;
        PlayerUnit.OnPlayerDied += DisableMovement;
    }

    public void OnDisable()
    {
        PlayerControls.OnMovement -= Move;
        PlayerUnit.OnPlayerDied -= DisableMovement;
    }
    
    private void DisableMovement()
    {
        canMove = false;
    }
    
    private void Move(float horizontal, float vertical)
    {
        // Get player's movement direction based on input and normalize to prevent double speed when moving diagonal
        direction = new Vector2(horizontal, vertical).normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = canMove ? new Vector2(direction.x * speed.actual, direction.y * speed.actual) : Vector2.zero;
    }
}