using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit
{
    public PlayerMovement playerMovement;
    private void Start()
    {
        // playerMovement
    }
    protected override void OnStateChanged(GameState state)
    {
        if (state == GameState.Playing)
        {
            // playerMovement.canMove = true;
            // playerMovement.ToggleMovement();
        }
    }
}