using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool canMove = true;
    public Rigidbody2D rb;
    public PlayerUnit unit;

    private float speed;

    private void Start()
    {
        // rb = GetComponent<Rigidbody2D>();
        // unit = GetComponent<PlayerUnit>();
        speed = unit.stats.speed;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(horizontal, vertical);
            rb.velocity = movement * (speed * Time.deltaTime);
        }
    }
}