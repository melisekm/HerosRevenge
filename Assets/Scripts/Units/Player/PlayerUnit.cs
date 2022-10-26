using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerUnit : Unit
{
    public PlayerStats stats { get; private set; }
    public void SetStats(PlayerStats st) => stats = st;



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
        // TODO TEMPORARY Spawn manager should set this or load it from somewhere
        ScriptablePlayer playerScriptable = ResourceSystem.Instance.player;
        SetAttributes(playerScriptable.attributes);
        SetStats(playerScriptable.playerStats);
    }

    protected override void Update()
    {
        Debug.Log(this.stats.level);
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