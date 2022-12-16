public class SurviveForTime : WinConditionChecker
{
    public float timeToSurvive = 120f;

    private void OnEnable()
    {
        GameManager.OnUpdateTime += Check;
    }

    private void OnDisable()
    {
        GameManager.OnUpdateTime -= Check;
    }

    private void Check(float time)
    {
        if (time >= timeToSurvive)
        {
            InvokeWin();
        }
    }
}
