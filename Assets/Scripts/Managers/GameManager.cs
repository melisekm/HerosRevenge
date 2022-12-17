using System;
using System.Collections;
using System.Collections.Generic;
using Units.Player;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // time until the game starts
    [Min(0.01f)] [SerializeField] private float startDelay = 2f;
    private int killCount;
    private PlayerContainer playerContainer;
    private PlayerUnit playerUnit;
    private List<Spawner> spawners = new();
    private GameState state;

    private void Awake()
    {
        var playerContainerGo = GameObject.FindGameObjectWithTag("PlayerContainer");
        if (playerContainerGo && playerContainerGo.TryGetComponent(out PlayerContainer playerContainer))
        {
            this.playerContainer = playerContainer;
        }
        else
        {
            Debug.LogError("PlayerContainer not found");
        }

        var spawnersGo = GameObject.FindGameObjectsWithTag("Spawner");
        foreach (var spawnerGo in spawnersGo)
        {
            var spawncomponents = spawnerGo.GetComponents<Spawner>();
            foreach (var spawncomponent in spawncomponents)
            {
                spawners.Add(spawncomponent);
            }
        }
    }

    private void Start()
    {
        ChangeState(GameState.Starting);
    }

    private void Update()
    {
        if (state == GameState.Playing)
        {
            OnUpdateTime?.Invoke(Time.timeSinceLevelLoad);
        }
    }

    private void OnEnable()
    {
        ProgressionController.OnLevelUp += OnLevelUp;
        LevelUpUISetter.OnRewardSelected += OnRewardSelected;
        UnitSpawner.OnEnemySpawned += OnEnemySpawned;
        EnemyUnit.OnEnemyUnitDied += OnEnemyDied;
        PlayerUnit.OnPlayerDied += OnPlayerDead;
        WinConditionChecker.OnWinConditionMet += OnWinConditionMet;
    }

    private void OnDisable()
    {
        ProgressionController.OnLevelUp -= OnLevelUp;
        LevelUpUISetter.OnRewardSelected -= OnRewardSelected;
        UnitSpawner.OnEnemySpawned -= OnEnemySpawned;
        EnemyUnit.OnEnemyUnitDied -= OnEnemyDied;
        PlayerUnit.OnPlayerDied -= OnPlayerDead;
        WinConditionChecker.OnWinConditionMet -= OnWinConditionMet;
    }

    public static event Action<float> OnUpdateTime;
    public static event Action<int> OnKillCountChange;

    private void OnPlayerDead()
    {
        ChangeState(GameState.ArenaFailed);
    }

    private void OnWinConditionMet()
    {
        ChangeState(GameState.ArenaFinished);
    }


    private void OnEnemyDied(EnemyUnit obj)
    {
        killCount++;
        OnKillCountChange?.Invoke(killCount);
        // TODO: if curr lvl has enemy cap and enemiesKilled == cap, change state to level complete
    }

    private void OnEnemySpawned(EnemyUnit enemy)
    {
        // TODO: check if enemy count in unitspawner is equal to enemy cap if yes lose
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

    private void ChangeState(GameState newState)
    {
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
        playerContainer.CompleteCurrentArena(killCount);
    }

    private void HandleArenaFailed()
    {
        playerContainer.FailCurrentArena(killCount);
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
            foreach (var spawner in spawners)
            {
                spawner.Activate();
            }
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