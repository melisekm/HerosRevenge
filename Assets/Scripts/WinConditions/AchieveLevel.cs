using UnityEngine;

public class AchieveLevel : WinConditionChecker
{
    [SerializeField] private PlayerUnit playerUnit;
    public float levelToAchieve;

    protected override void Awake()
    {
        base.Awake();
        if (newGamePlus > 0)
        {
            levelToAchieve = playerUnit.stats.level.actual + 2;
        }

        description = "Reach level " + levelToAchieve;
    }

    private void OnEnable()
    {
        LevelUpUISetter.OnRewardSelected += Check;
    }

    private void OnDisable()
    {
        LevelUpUISetter.OnRewardSelected -= Check;
    }

    private void Check(ScriptableReward _)
    {
        if (playerUnit.stats.level.actual >= levelToAchieve)
        {
            InvokeWin();
        }
    }
}