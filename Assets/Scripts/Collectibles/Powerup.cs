using UnityEngine;

public abstract class Powerup : Collectible, INitializableCollectible
{
    public float duration = 5f;
    protected float disappearTime = 15f;

    protected override void Update()
    {
        if (disappearTime > 0)
        {
            disappearTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }

        base.Update();
    }

    public abstract void Initialize(ScriptablePowerUp powerup, float disappearTime);
}

public interface INitializableCollectible
{
    public void Initialize(ScriptablePowerUp powerup, float disappearTime);
}