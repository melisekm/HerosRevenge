using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    protected GameObject player;
    private float pickupDistance;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pickupDistance = player.GetComponent<PlayerUnit>().pickupRange;
    }

    private void Update()
    {
        if (!player) return;
        if (Vector3.Distance(transform.position, player.transform.position) < 1)
        {
            PickUp();
            Destroy(gameObject);
        }
    }

    protected abstract void PickUp();
}
