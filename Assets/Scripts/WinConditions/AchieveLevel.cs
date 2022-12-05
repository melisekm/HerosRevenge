using UnityEngine;

public class AchieveLevel : WinConditionChecker
{
    [SerializeField] private PlayerUnit playerUnit;
    public float levelToAchieve;

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