using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DedUnit : EnemyUnit
{
    protected override void Update()
    {
        base.Update();
        sprite.flipX = destinationSetter.target.position.x > transform.position.x;
    }
}
