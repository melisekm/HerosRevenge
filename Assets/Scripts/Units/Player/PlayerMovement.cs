using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool canMove = true;
    public PlayerUnit unit;
    private Vector2 direction;


    private void Update()
    {
        // Get player's movement direction based on input and normalize to prevent double speed when moving diagonal
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }


    private void FixedUpdate()
    {
        if (canMove)
        {
            float speed = unit.attributes.speed.actual;
            unit.rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
        }
    }
}