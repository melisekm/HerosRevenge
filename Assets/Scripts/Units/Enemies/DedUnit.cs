using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DedUnit : RangedEnemyUnit
{
    private bool isEvading;

    protected override void AttackPlayer()
    {
        base.AttackPlayer();
        if (!isEvading)
        {
            StartCoroutine(EvadePlayer());
        }
    }

    private IEnumerator EvadePlayer()
    {
        isEvading = true;
        destinationSetter.enabled = false;
        var oldEndReachedDistance = aiPath.endReachedDistance;
        aiPath.endReachedDistance = 0;
        // set random position near the gameObject
        var randomPosition = transform.position + Random.insideUnitSphere * 5;
        aiPath.destination = randomPosition;
        yield return new WaitForSeconds(2f);
        // revert to normal
        aiPath.endReachedDistance = oldEndReachedDistance;
        destinationSetter.enabled = true;
        isEvading = false;
    }
}