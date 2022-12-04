using UnityEngine;

public abstract class Powerup : Collectible, InitializableCollectible
{
    public float disapearTime = 15f;
    public float duration = 5f;

    protected override void Update()
    {
        if (disapearTime > 0)
        {
            disapearTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }

        base.Update();
    }

    public abstract void Initialize(ScriptablePowerUp powerup);
}

public interface InitializableCollectible
{
    public void Initialize(ScriptablePowerUp powerup);
}