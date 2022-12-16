using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerUnit))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public bool canMove = true;
    public float shootSlowdown = 0.5f;
    private Vector2 direction;
    private bool isShooting;
    private PlayerUnit playerUnit;
    private Rigidbody2D rb;
    private Attribute speed;

    private void Start()
    {
        playerUnit = GetComponent<PlayerUnit>();
        rb = GetComponent<Rigidbody2D>();
        speed = playerUnit.attributes.speed;
    }

    private void FixedUpdate()
    {
        rb.velocity = canMove ? new Vector2(direction.x * speed.actual, direction.y * speed.actual) : Vector2.zero;
    }


    public void OnEnable()
    {
        PlayerControls.OnMovement += Move;
        PlayerControls.OnAttack += SlowDownMovement;
        PlayerUnit.OnPlayerDied += DisableMovement;
    }

    public void OnDisable()
    {
        PlayerControls.OnMovement -= Move;
        PlayerControls.OnAttack -= SlowDownMovement;
        PlayerUnit.OnPlayerDied -= DisableMovement;
    }

    private void SlowDownMovement()
    {
        IEnumerator SlowDown()
        {
            isShooting = true;
            speed.actual = speed.actual * shootSlowdown;
            yield return new WaitForSeconds(0.1f);
            speed.actual = speed.initial;
            isShooting = false;
        }

        if (!isShooting)
            StartCoroutine(SlowDown());
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
}