using System.Collections;
using UnityEngine;

public class EventSpawner : EffectSpawner
{
    public bool isEventActivable = true;
    [Header("Event")] public int aoeChance = 10;
    public int checkInterval = 10;
    public int length = 10;
    private bool isEventActive;

    private ScriptableEffect eventEffect;
    // private float spawn


    private void Start()
    {
        StartCoroutine(EventScheduler());
    }

    private IEnumerator EventScheduler()
    {
        while (true)
        {
            if (isEventActivable)
            {
                // random 0 - 100
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
            Debug.Log("Starting event");
            yield return new WaitForSeconds(length);
            Debug.Log("Ending event");
            isEventActive = false;
        }

        StartCoroutine(ToggleAoEEvent());
    }

    protected override void Update()
    {
        if (!isEventActive) return;
        base.Update();
    }

    protected override ScriptableObject GetEntity()
    {
        return eventEffect;
    }
}