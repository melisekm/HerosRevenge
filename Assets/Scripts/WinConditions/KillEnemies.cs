using UnityEngine;

public class KillEnemies : WinConditionChecker
{
    public float killsToWin = 10;

    private void OnEnable()
    {
        GameManager.OnKillCountChange += Check;
    }

    private void OnDisable()
    {
        GameManager.OnKillCountChange -= Check;
    }

    private void Check(int killCount)
    {
        if (killCount >= killsToWin)
        {
            InvokeWin();
        }
    }
}