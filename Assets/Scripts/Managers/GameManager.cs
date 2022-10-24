using System;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    public GameState state;

    // time until the game starts
    [Min(0.01f)]
    [SerializeField]
    private float startDelay = 2f;
    [SerializeField] private bool spawnPlayer = false;


    private void Start()
    {
        ChangeState(GameState.Starting);
    }

    public void ChangeState(GameState newState)
    {
        OnBeforeStateChanged?.Invoke(newState);

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

        OnAfterStateChanged?.Invoke(newState);

        Debug.Log($"New state: {newState}");
    }

    private void HandleArenaFinished()
    {
        throw new NotImplementedException();
    }

    private void HandleArenaFailed()
    {
        throw new NotImplementedException();
    }

    private void HandleStarting()
    {
        if (spawnPlayer) SpawnManager.Instance.SpawnPlayer();
        ChangeState(GameState.Playing);
    }

    private void HandlePlaying()
    {
        TimerManager.Instance.StartTimer(SpawnManager.Instance.SpawnEnemies, startDelay);
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