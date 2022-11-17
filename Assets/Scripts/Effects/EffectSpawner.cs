using System.Collections;
using UnityEngine;
using Utils;

public class EffectSpawner : MonoBehaviour
{
    public float spawnEffectEverySeconds = 5f;
    private float timer;
    public bool isActive = true;
    public Vector2 spawnArea;
    public int areaOfEffectEventChance = 10;
    public int eventCheckInterval = 10;
    public int eventLength = 10;


    private void Start()
    {
        StartCoroutine(EventScheduler());
    }

    private IEnumerator EventScheduler()
    {
        while (true)
        {
            if (isActive)
            {
                // random 0 - 100
                var random = Random.Range(0, 100);
                if (random < areaOfEffectEventChance)
                {
                    AoEEvent();
                }
            }

            yield return new WaitForSeconds(eventCheckInterval);
        }
    }

    private void AoEEvent()
    {
        IEnumerator ToggleAoEEvent()
        {
            var previousSpawnEffectEverySeconds = spawnEffectEverySeconds;
            spawnEffectEverySeconds = 0.1f;
            timer = 0;
            Debug.Log("Starting event");
            yield return new WaitForSeconds(eventLength);
            Debug.Log("Ending event");
            spawnEffectEverySeconds = previousSpawnEffectEverySeconds;
        }

        StartCoroutine(ToggleAoEEvent());
    }


    private void Update()
    {
        if (!isActive) return;

        if (timer <= 0)
        {
            SpawnEffect();
            timer = spawnEffectEverySeconds;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    private void SpawnEffect()
    {
        ScriptableEffect scriptableEffect = ResourceSystem.Instance.GetRandomEffect();
        // get random position in a rectangle
        Vector3 position = new Vector3(
            Random.Range(-spawnArea.x, spawnArea.x), Random.Range(-spawnArea.y, spawnArea.y), 0);
        // spawn effect
        if (scriptableEffect.effectType == EffectType.Damage)
        {
            Effect effect = Instantiate(scriptableEffect.effect, position, Quaternion.identity);
            
            effect.Initialize(scriptableEffect, Faction.Player);
        }
    }
}