using System.Collections;
using UnityEngine;

public class EventSpawner : EffectSpawner
{
    [Header("Event")] public int aoeChance = 10;
    public int checkInterval = 10;
    public int length = 10;

    private ScriptableEffect eventEffect;

    private bool isEventActive;
    // private float spawn


    private void Start()
    {
        StartCoroutine(EventScheduler());
    }

    protected override void Update()
    {
        if (!isEventActive) return;
        base.Update();
    }


    private IEnumerator EventScheduler()
    {
        while (true)
        {
            if (isSpawnerActive)
            {
                var random = Random.Range(0, 100);
                if (random < aoeChance)
                {
                    AoEEvent();
                }
            }

            yield return new WaitForSeconds(checkInterval);
        }
    }

    private void AoEEvent()
    {
        IEnumerator ToggleAoEEvent()
        {
            isEventActive = true;
            timeUntilSpawn = 0;
            eventEffect = ResourceSystem.Instance.GetEffectByType(EffectType.DangerIndicator);
            yield return new WaitForSeconds(length);
            isEventActive = false;
        }

        StartCoroutine(ToggleAoEEvent());
    }

    protected override ScriptableObject GetEntity()
    {
        return eventEffect;
    }
}