using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerUnit))]
public class PlayerMovement : MonoBehaviour
{
    public bool canMove = true;
    public PlayerUnit playerUnit;
    private Vector2 direction;
    

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
        if (canMove)
        {
            float speed = playerUnit.attributes.speed.actual;
            playerUnit.rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
        }
        else
        {
            playerUnit.rb.velocity = Vector2.zero;
        }
    }
}