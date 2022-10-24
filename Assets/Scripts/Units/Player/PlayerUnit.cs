using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    protected override void Start()
    {
        base.Start();
        var playerScriptable = ResourceSystem.Instance.player;
        SetStats(playerScriptable.stats);
    }

    protected override void Update()
    {
        base.Update();
        if (rb.velocity.x >= 0.01f)
        {
            if (!isFacingRight)
            {
                Flip();
            }
        
        }
        else if(rb.velocity.x <= -0.01f)
        {
            if (isFacingRight)
            {
                Flip();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnManager.Instance.ToggleSpawning();
        }
    }
    
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        sprite.flipX = !sprite.flipX;
    }

}