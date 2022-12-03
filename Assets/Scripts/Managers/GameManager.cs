using System;
using System.Collections;
using System.Collections.Generic;
using Units.Player;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Dictionary<int, EnemyUnit> enemies = new();
    public int enemiesKilled;


    public UnitSpawner unitSpawner;
    public GameState state;

    // time until the game starts
    [Min(0.01f)] [SerializeField] private float startDelay = 2f;

    public static event Action<float> OnUpdateTime;
    

    private void Start()
    {
        ChangeState(GameState.Starting);
    }

    private void OnEnable()
    {
        ProgressionController.OnLevelUp += OnLevelUp;
        LevelUpUISetter.OnRewardSelected += OnRewardSelected;
        UnitSpawner.OnEnemySpawned += OnEnemySpawned;
        EnemyUnit.OnEnemyUnitDied += OnEnemyDied;
        PlayerUnit.OnPlayerDied += OnPlayerDead;
    }

    private void OnDisable()
    {
        ProgressionController.OnLevelUp -= OnLevelUp;
        LevelUpUISetter.OnRewardSelected -= OnRewardSelected;
        UnitSpawner.OnEnemySpawned -= OnEnemySpawned;
        EnemyUnit.OnEnemyUnitDied -= OnEnemyDied;
        PlayerUnit.OnPlayerDied -= OnPlayerDead;
    }

    private void OnPlayerDead()
    {
        ChangeState(GameState.ArenaFailed);
    }


    private void OnEnemyDied(EnemyUnit obj)
    {
        enemies.Remove(obj.GetInstanceID());
        enemiesKilled++;
        // TODO: if curr lvl has enemy cap and enemiesKilled == cap, change state to level complete
    }

    private void OnEnemySpawned(EnemyUnit enemy)
    {
        enemies[enemy.GetInstanceID()] = enemy;
    }


    private void OnRewardSelected(ScriptableReward reward)
    {
        Time.timeScale = 1;
    }

    private void OnLevelUp(PlayerStats stats, Attributes attributes, bool initial,
        RewardGenerator.Reward[] rewards)
    {
        // freeze the game
        if (!initial && state == GameState.Playing)
        {
            Time.timeScale = 0;
        }
    }

    private void Update()
    {
        if (state == GameState.Playing)
        {
            OnUpdateTime?.Invoke(Time.timeSinceLevelLoad);
        }
    }

    private void ChangeState(GameState newState)
    {
        Debug.Log($"New state: {newState}");

        state = newState;
        switch (newState)
        {
            case GameState.Starting:
                HandleStarting();
                break;
            case GameState.Playing:
                HandlePlaying();
                break;
            case GameState.ArenaFailed:
                HandleArenaFailed();
                break;
            case GameState.ArenaFinished:
                HandleArenaFinished();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    private void HandleArenaFinished()
    {
    }

    private void HandleArenaFailed()
    {
    }

    private void HandleStarting()
    {
        ChangeState(GameState.Playing);
    }

    private void HandlePlaying()
    {
        IEnumerator StartSpawning()
        {
            yield return new WaitForSeconds(startDelay);
            unitSpawner.SpawnEnemies();
        }

        StartCoroutine(StartSpawning());
    }
}

[Serializable]
public enum GameState
{
    Starting = 0,
    Playing = 1,
    ArenaFailed = 2,
    ArenaFinished = 3
}