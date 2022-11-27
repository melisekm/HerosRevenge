using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    protected GameObject player;
    private PlayerUnit playerUnit;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerUnit = player.GetComponent<PlayerUnit>();
    }

    protected virtual void Update()
    {
        if (!player) return;
        
        if (Vector3.Distance(transform.position, player.transform.position) < playerUnit.attributes.pickupRange.actual)
        {
            PickUp();
        }
    }

    protected abstract void PickUp();
}
