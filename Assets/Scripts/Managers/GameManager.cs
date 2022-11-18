using System;
using System.Collections;
using Managers;
using Units.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameObject player;
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    public GameState state;

    // time until the game starts
    [Min(0.01f)] [SerializeField] private float startDelay = 2f;
    [SerializeField] private bool spawnPlayer = false;

    private float currentTime;

    public delegate void UpdateUI(float value);

    public static event UpdateUI OnUpdateTime;


    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player");
    }


    private void Start()
    {
        ChangeState(state);
    }

    private void OnEnable()
    {
        ProgressionController.OnLevelUp += OnLevelUp;
        LevelUpUISetter.OnRewardSelected += OnRewardSelected;
    }

    private void OnDisable()
    {
        ProgressionController.OnLevelUp -= OnLevelUp;
        LevelUpUISetter.OnRewardSelected -= OnRewardSelected;
    }

    private void OnRewardSelected(ScriptableReward reward)
    {
        Time.timeScale = 1;
    }

    private void OnLevelUp(PlayerStats stats, Attributes attributes, bool initial,
        LevelUpSelectionHandler.Reward[] rewards)
    {
        // freeze the game
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (state == GameState.Playing)
        {
            currentTime = Time.timeSinceLevelLoad;
            OnUpdateTime?.Invoke(currentTime);
        }
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
        IEnumerator PlayerDeath()
        {
            if (player && player.TryGetComponent(out PlayerMovement playerMovement) &&
                player.TryGetComponent(out PlayerUnit playerUnit))
            {
                Time.timeScale = 0.0f;
                playerMovement.canMove = false;
                playerUnit.sprite.enabled = false;
                UIManager.Instance.deathText.gameObject.SetActive(true);
                yield return new WaitForSecondsRealtime(2f);
                Time.timeScale = 1;
                SceneManager.LoadScene(0);
            }
        }

        StartCoroutine(PlayerDeath());
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