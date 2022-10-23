using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit
{
    public PlayerMovement playerMovement;
 
    protected override void OnStateChanged(GameState state)
    {
        if (state == GameState.Playing)
        {
            // this happens before the player movement script is enabled
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnManager.Instance.ToggleSpawning();
        }
    }
}