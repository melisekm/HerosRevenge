using System.Collections;
using UnityEngine;

public class DedUnit : RangedEnemyUnit
{
    public float evadeTime = 2f;
    public float randomPathSize = 5f;
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
        var randomPosition = transform.position + Random.insideUnitSphere * randomPathSize;
        aiPath.destination = randomPosition;
        yield return new WaitForSeconds(evadeTime);
        // revert to normal
        aiPath.endReachedDistance = oldEndReachedDistance;
        destinationSetter.enabled = true;
        isEvading = false;
    }
}